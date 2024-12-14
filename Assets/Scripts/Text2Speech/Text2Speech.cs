using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class Text2Speech : MonoBehaviour
{

    [SerializeField] private AudioSource audiosource;

    private bool isAudioPlaying = false;
    public async void ConvertTextToSpeech(string text)
    {
        if (isAudioPlaying)
        {
            await WaitForAudioToFinish();
        }
        var credentials = new BasicAWSCredentials("", "");
        var client = new AmazonPollyClient(credentials, RegionEndpoint.EUWest3);

        var request = new SynthesizeSpeechRequest()
        {
            Text = text,
            Engine = Engine.Neural,
            VoiceId = VoiceId.Stephen,
            OutputFormat = OutputFormat.Mp3
        };

        var response = await client.SynthesizeSpeechAsync(request);

        WriteIntoFile(response.AudioStream);

        using (var www =  UnityWebRequestMultimedia.GetAudioClip($"{Application.persistentDataPath}/audio.mp3", AudioType.MPEG))
        {
            var op = www.SendWebRequest();

            while (!op.isDone) await Task.Yield();

            var clip = DownloadHandlerAudioClip.GetContent(www);

            audiosource.clip = clip;
            audiosource.Play();
            isAudioPlaying = true;

            await WaitForAudioToFinish();

        }
    }

    private void WriteIntoFile(Stream stream)
    {
        using (var fileStream = new FileStream($"{Application.persistentDataPath}/audio.mp3", FileMode.Create)) {

            byte[] buffer = new byte[8 * 1024];
            int bytesRead = 0;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0 ){

                fileStream.Write(buffer, 0, bytesRead);
            }
        }
    }
    private async Task WaitForAudioToFinish()
    {
        while (audiosource.isPlaying)
        {
            await Task.Yield(); 
        }

        isAudioPlaying = false;
    }
}
