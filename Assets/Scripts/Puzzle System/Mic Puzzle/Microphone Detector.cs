using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneDetector : MonoBehaviour
{
    public string microphoneDevice; // Optional: specify the microphone device name
    public float volumeThreshold = 0.1f; // Set the volume threshold
    public bool isLoudEnough = false; // Boolean to track if volume is above threshold

    private AudioClip microphoneClip;
    private int sampleWindow = 128; // Number of samples to analyze for volume detection

    [SerializeField] private Interactable interactable;

    private void Start()
    {
        // Start microphone recording with the specified device, if available
        if (microphoneDevice == "")
        {
            microphoneDevice = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;
        }

        if (microphoneDevice != null)
        {
            microphoneClip = Microphone.Start(microphoneDevice, true, 1, 44100);
            Debug.Log("Mic->" + microphoneDevice);
        }
        else
        {
            Debug.LogError("No microphone found!");
        }
    }

    private void Update()
    {
        if (interactable != null)
        {
            if (interactable.WasTriggered())
            {
                if (microphoneClip != null)
                {
                    isLoudEnough = DetectLoudness();
                    Debug.Log("STARTED");
                }
                if (isLoudEnough)
                {
                    Debug.Log("Volume threshold exceeded!");
                }

            }
        }
        else
        {
            Debug.LogWarning("Interactable reference is not assigned in MicrophoneDetector.");
        }
        
    }

    private bool DetectLoudness()
    {
        float[] audioData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(microphoneDevice) - sampleWindow + 1;
        if (micPosition < 0) return false;

        microphoneClip.GetData(audioData, micPosition);

        // Calculate RMS (Root Mean Square) volume of the audio sample
        float sum = 0f;
        for (int i = 0; i < sampleWindow; i++)
        {
            sum += audioData[i] * audioData[i];
        }
        float rmsValue = Mathf.Sqrt(sum / sampleWindow);

        // Check if the volume exceeds the threshold
        return rmsValue >= volumeThreshold;
    }

    private void OnDestroy()
    {
        // Stop the microphone when the object is destroyed
        if (microphoneDevice != null)
        {
            Microphone.End(microphoneDevice);
        }
    }
}
