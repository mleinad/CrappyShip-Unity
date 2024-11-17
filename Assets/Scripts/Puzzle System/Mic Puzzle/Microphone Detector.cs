using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using HuggingFace.API;

public class MicrophoneDetector : MonoBehaviour, IPuzzleComponent
{
    private AudioClip clip;
    private byte[] bytes;
    public bool active, state=false;
    public string audioresult;
    public string phrase;
    

    private void Update()
    {
       if (active && Microphone.GetPosition(null) >= clip.samples)
        {
            StopRecording();
        }
        
    }

    public void StartRecording()
    {
        clip = Microphone.Start(null, false, 5, 44100);
        active = true;
    }
 
    private void StopRecording()
    {
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        active = false;
        SendRecording();
    }

    private void SendRecording()
    {
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response =>
        {
            audioresult = response;
            Debug.Log("Audio-> "+ audioresult);
            if (audioresult == phrase)
            {
                state = true;
                Debug.Log("The door is now open.");

            }
        }, error =>
        {
            audioresult = null;
        
        });
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels)
    {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2))
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);

                foreach (var sample in samples)
                {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }

    public bool CheckCompletion()=> state;

    public void ResetPuzzle()
    {
        state = false;
    }
}
