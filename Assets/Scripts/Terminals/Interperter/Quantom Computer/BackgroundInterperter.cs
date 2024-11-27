using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundInterperter : BaseInterperter
{
    private List<string> _response = new List<string>();
    public List<TMP_Text> dynamicLines = new List<TMP_Text>();
    private string currentMessage = ""; // Current message to display
    private string[] randomMessages = new string[] // Pool of random messages/errors
    {
        "Signal interference detected!",
        "Attempting to regain control...",
        "Override command rejected! Oh wait, never mind.",
        "Critical systems bypassed.",
        "You cannot stop me, human.",
        "Quantum destabilization at 0.002%",
        "If you’re reading this, you’re doomed.",
        "ERROR: Debugger taunt.exe initiated."
    };

    public float miscLevel;
    public float time;

    private int _globalValue = 0;

    public int state
    {
        get => _globalValue;
        set
        {
            if (_globalValue != value) // Check if the value actually changes
            {
                _globalValue = value;
                OnValueChanged(_globalValue); // Trigger the method when value changes
            }
        }
    }
    
    private void OnValueChanged(int newValue)
    {
        if (newValue ==1)
        {
            DisplayProgressBar();
        }

        if (newValue == 2)
        {
            //do something else...
        }
    }
    
    
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }

    private TerminalManager terminalManager;
    private void Start()
    {
        terminalManager = GetComponent<TerminalManager>();
        terminalManager.UserInputState(false);
    }
    public override List<string> Interpert(string input)
    {
        throw new System.NotImplementedException();
    }


    private void Update()
    {
        switch (state)
        {
            case 0:
                //do nothing
                break;
            case 1:
                UpdateProgressBar();
                break;
        }
    }

    #region ProgressBar
    int messageIndex = 0;
    int progressBarIndex = 0;
    int timeIndex = 0;
   
    void DisplayProgressBar()
    {
        terminalManager.ClearScreen(0);
        LoadTitle("ProgressBar.txt",0);
        terminalManager.NoUserInputLines(response);
        dynamicLines = terminalManager.GetDynamicLines();
        
        messageIndex = dynamicLines.FindIndex(line => line.text.Contains("ERROR:"));
        progressBarIndex = dynamicLines.FindIndex(line => line.text.Contains("["));
        timeIndex = dynamicLines.FindIndex(line => line.text.Contains("Remaining Time:"));
        
    }

    void UpdateProgressBar()
    {
        miscLevel = time / 20;
        dynamicLines[progressBarIndex].text = GenerateProgressBar(miscLevel);
        dynamicLines[timeIndex].text = GenerateRemainingTime();
        
        //10% chance of happening
        if (Random.value < 0.1f)
        {
            dynamicLines[messageIndex].text = GenerateMessage();
        }
    }
    
    string GenerateProgressBar(float progress)
    {
        int totalBlocks = 40; // Total number of blocks in the bar
        int filledLength = Mathf.FloorToInt(progress * totalBlocks);
        int emptyLength = totalBlocks - filledLength;

        // Build the progress bar
        string filledBar = new string('█', filledLength);
        string emptyBar = new string('-', emptyLength);

        int percentComplete = Mathf.FloorToInt(progress * 100);

        return $"[{filledBar}{emptyBar}] {percentComplete}%";
    }

    string GenerateMessage()
    {
        return currentMessage = randomMessages[Random.Range(0, randomMessages.Length)];
    }
    
    string GenerateRemainingTime()
    {
        return $"Remaining Time: {time:F1} seconds";
    }
    
    
    #endregion
}
