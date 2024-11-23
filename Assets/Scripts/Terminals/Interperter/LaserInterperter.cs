using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LaserInterper : BaseInterperter, IPuzzleComponent
{
    
    private List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }
    TerminalManager terminalManager;
    private string garbledText = "";
    private Dictionary<char, char> charMap; // Mapping for consistent scrambling
    private readonly string allCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    bool state = false;
    public string typedWord;
    
    void Start()
    {
        terminalManager = GetComponent<TerminalManager>();
        
        
        terminalManager.terminal_input.onValueChanged.AddListener(OnInputChanged);
        GenerateCharacterMap(3);
    }

    public override List<string> Interpert(string input)
    {
        response.Clear();
        string[] args = input.Split();
        if (args[0] == "help")
        {
            response.Add("");
            return response;
        }
        if (args[0] == "open")
        {
            response.Add("opened door...");
            state = true;
            EventManager.Instance.OnTriggerSolved(this);
            return response;
        }
        else
        {
            response.Add("unknown command");
            return response;
        }
    }

    void GenerateCharacterMap(int shift)
    {
        // Initialize the map with a constant scrambling rule
        charMap = new Dictionary<char, char>();

        foreach (char c in allCharacters)
        {
            // Map each character to a "shifted" counterpart, wrapping around if necessary
            if (char.IsLetterOrDigit(c))
            {
                char scrambledChar = (char)((c + shift) % 127);
                if (!char.IsLetterOrDigit(scrambledChar)) // Ensure scrambled characters are printable
                {
                    scrambledChar = (char)((scrambledChar + '0') % 127);
                }

                charMap[c] = scrambledChar;
            }
        }
    }

    private void Update()
    {
        typedWord = terminalManager.terminal_input.text;
    }

    void OnInputChanged(string input)
    {
        garbledText = "";

        // Loop through the entire input and scramble each character consistently
        
        for(int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (char.IsControl(c))  // Skip control characters like backspace
            {
                garbledText += c;
                continue;
            }
            if (charMap.TryGetValue(c, out var value))
            {
                if (typedWord.Length == i)  // Only scramble the current character
                {
                    garbledText += value;
                }
                else
                {
                    garbledText += typedWord[i];  // Keep the already scrambled character
                }
                
            }
            else
            {
                garbledText += c;
            }
        }

        terminalManager.terminal_input.onValueChanged.RemoveListener(OnInputChanged);

        terminalManager.terminal_input.text = garbledText;

        terminalManager.terminal_input.onValueChanged.AddListener(OnInputChanged);

        terminalManager.terminal_input.caretPosition = input.Length;
    }

    public bool CheckCompletion()
    {
        return state;
    }

    public void ResetPuzzle()
    {
       state = false;
    }
}

