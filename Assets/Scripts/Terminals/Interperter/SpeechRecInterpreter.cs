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
            //Add Delay and Clear Console
            response.Add("Voice Commands:");
            response.Add("-Open door");
            response.Add("-Close door");
            //Add Delay
            microphoneDetector.StartRecording();
            response.Add("Listening");
            response.Add("...");
            response.Add("...");
            if (microphoneDetector.state)
            {
                response.Add("Open door command activated");
                //AudioClip open the door
                return response;
            }
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
