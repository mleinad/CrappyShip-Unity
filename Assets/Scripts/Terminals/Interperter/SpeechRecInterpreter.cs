using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;

public class SpeechRecInterpreter : BaseInterperter
{
    public TerminalManager terminalManager;
    public MicrophoneDetector microphoneDetector;
    [SerializeField] private Interactable interactable;
    private List<TMP_Text> terminalUI;

    List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }
    private bool isListening = false;

    private void Start()
    {
        interactable.enabled = false;
        terminalManager.NoUserInputLines(LoadTitle("micUI.txt", "white", 0));
        terminalManager.UserInputState(false);
        terminalUI = terminalManager.GetDynamicLines();
        WaitForMicrophoneInput();

    }
 
    public override List<string> Interpert(string input)
    {
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
           //Change something to Door Activated 
            interactable.enabled = true;
        }
        else
        {
            //Change for Voice Command not Recognized
            
        }

        isListening = false; 
    }

    private IEnumerator ExtendedListeningWindow(int seconds)
    {

        for (int i = 1; i <= seconds; i++)
        {
                //CHANGE (SECONDS) TO THE NUMBER OF SECONDS REMAINING THEN CHANGE THE MIC ACTIVATED
                yield return new WaitForSeconds(1);
        }
    }
}


