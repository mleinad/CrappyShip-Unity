using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class SpeechRecInterpreter : BaseInterperter
{
    public MicrophoneDetector microphoneDetector;
    [SerializeField] private Interactable interactable;

    List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }
    TerminalManager terminalManager;
    private bool isListening = false;

    private void Start()
    {
        interactable.enabled = false;

    }
    private void Awake()
    {
        
        terminalManager = GetComponent<TerminalManager>();
    }


    public override List<string> Interpert(string input)
    {
        response.Clear();
        string[] args = input.Split();

            terminalManager.NoUserInputLines(new List<string> {
            "Keyboard malfunction, please use voice commands",
            "                                                         Voice Commands:",
            " -Open door                               "

            });
            
            DelayMessage(2f, "To activate the mirophone to use voice commands press <V>");

            StartCoroutine(WaitForMicrophoneInput());

            return response;
        }

    
    private void DelayMessage(float delay, string message)
    {
        StartCoroutine(DelayMessageCoroutine(delay, message));
    }

    private IEnumerator DelayMessageCoroutine(float delay, string message)
    {
        yield return new WaitForSeconds(delay);
        terminalManager.NoUserInputLines(new List<string> { message });
    }
    private IEnumerator WaitForMicrophoneInput()
    {
        // Wait for the "V" key to be pressed
        while (!Input.GetKeyDown(KeyCode.V))
        {
            yield return null; // Wait for the next frame
        }
        microphoneDetector.StartRecording();
        isListening = true;

        yield return StartCoroutine(ExtendedListeningWindow(8));

        
        if (microphoneDetector.state)
        {
            terminalManager.NoUserInputLines(new List<string> { "Open door command activated" });
            interactable.enabled = true;
        }
        else
        {
            terminalManager.NoUserInputLines(new List<string> { "No valid voice command detected. Please try again." });
        }

        isListening = false; 
    }

    private IEnumerator ExtendedListeningWindow(int seconds)
    {

        for (int i = 1; i <= seconds; i++)
        {
                terminalManager.NoUserInputLines(new List<string> { $"Listening... {seconds - i} seconds remaining" });
                yield return new WaitForSeconds(1);
        }
    }
}


