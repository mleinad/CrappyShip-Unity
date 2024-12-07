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
    public bool midcheck =false;
    

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
 
    public void StopRecording()
    {
        if (!Microphone.IsRecording(null))
        {
            Debug.LogWarning("Microphone is not recording. StopRecording called prematurely.");
            return;
        }

        var position = Microphone.GetPosition(null);
        Debug.Log($"Microphone recording stopped. Position: {position}");

        Microphone.End(null);

        // Validate the audio clip
        if (clip == null || !clip.isReadyToPlay)
        {
            Debug.LogError("Audio clip is invalid or not ready after stopping microphone.");
            return;
        }

        position = Mathf.Min(position, clip.samples);

        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);

        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);

        active = false;
        Debug.Log($"Recording processed: {samples.Length} samples.");
        SendRecording();
    }

    private void SendRecording()
    {
        midcheck = true;
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response =>
        {
            audioresult = response;
            Debug.Log("Audio-> "+ audioresult);
          
                if (!string.IsNullOrEmpty(audioresult) &&
             (audioresult.IndexOf(" open door", StringComparison.OrdinalIgnoreCase) >= 0 ||
              audioresult.IndexOf(" open the door", StringComparison.OrdinalIgnoreCase) >= 0))
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
