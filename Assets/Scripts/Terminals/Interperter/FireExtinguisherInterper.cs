using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FireExtinguisherInterper : MonoBehaviour, Iinterperter
{
    readonly List<string> response = new List<string>();
    TerminalManager terminalManager;
    private string garbledText = "";
    private Dictionary<char, char> charMap; // Mapping for consistent scrambling
    private readonly string allCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public string typedWord;
   
    public Interactable fe_case;
    public DragNDrop fe_gun;
    void Start()
    {
        terminalManager = GetComponent<TerminalManager>();
        terminalManager.terminal_input.onValueChanged.AddListener(OnInputChanged);
        GenerateCharacterMap(3);
        fe_case.enabled = false;
        fe_gun.enabled = false;
    }

    public List<string> Interpert(string input)
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
            fe_case.enabled = true;

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
        if (fe_case.WasTriggered())
        {
            fe_gun.enabled = true;
        }
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
    
}

