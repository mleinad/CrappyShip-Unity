using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class SpeechRecInterpreter : MonoBehaviour, Iinterperter
{
    public MicrophoneDetector microphoneDetector;
    List<string> response = new List<string>();
    TerminalManager terminalManager;

    private void Awake()
    {
        terminalManager = GetComponent<TerminalManager>();
    }

    public List<string> Interpert(string input)
    {
        response.Clear();
       
        string[] args = input.Split();

    

        if (args[0] == "open")
        {
            response.Add("Keyboard malfunction, please use voice commands");
            response.Add("Voice Commands:");
            response.Add("-Open door");
            response.Add("-Close door");
            microphoneDetector.StartRecording();
            return response;
        }
        else
        {
            response.Add("Keyboard malfunction, please use voice commands");
            response.Add("Voice Commands:");
            response.Add("--Open door");
            response.Add("--Close door");
            microphoneDetector.StartRecording();
            return response;

        }
    }
    
    
}
