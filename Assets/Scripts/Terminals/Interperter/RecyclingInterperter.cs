using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class RecyclingInterperter : BaseInterperter
{
    private List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }

    [SerializeField] GameObject puzzle_component_gameobject;
    PressurePlate garbage_bin;
    TerminalManager terminalManager;
    [Range(0, 100)] public int contaminationLevel = 0;
    [SerializeField] private List<Interactable> interactable;

    private int progresBarIndex;
    private int roomStatusIndex;
    private List<TMP_Text> terminalUI;

    private bool page = false;

    void Start()
    {
        terminalManager = GetComponent<TerminalManager>();
  
        response.Add(" PRESS [ESC] TO EXIT                              ");
        terminalManager.NoUserInputLines(response);
        response.Clear();
        garbage_bin = puzzle_component_gameobject.GetComponent<PressurePlate>();

        foreach (Interactable i in interactable)
        {
            i.enabled = false;
        }
    }

    public override List<string> Interpert(string input)
    {
        response.Clear();
        input = input.ToLower();

        string[] args = input.Split(); 
        if (args[0] == "help")
        {
            ListEntry("run", "run a program from the terminal");
            ListEntry("storage", "lists installed programs");
            ListEntry("install", "install a new program");
            return response;
        }
        if (args[0] == "run")
        {
            if (args[1] == "manager.exe")
            {

                RunPage();
                //puzzle logic      maybe use events...
                if (!garbage_bin.CheckCompletion())
                {
                    //response.Add("checking room contamination...");
                    //response.Add(BoldString("Unable to open door, contamination risk too high!"));
                }
                else
                {
                    //response.Add("Room unlocked...");
                    Solved();
                }
                return response;
            }
            if (args[1] == "decrypter.exe")
            {
                response.Add("decrypter program... beep boop bzzzz");
                return response;
            }

            response.Add("executable not found");
            return response;
        }
        else
        {
            response.Add("Command not recognized.");
            return response;
        }
    }
    

    void ListEntry(string a, string b)
    {
        response.Add(ColorString(a, colors["orange"]) + ":" + ColorString(b, colors["yellow"]));
    }
    
    void Solved()
    {
        foreach (Interactable i in interactable)
        {
            i.enabled = true;
        }
    }


    private void Update()
    {
        if (page)
        {
            UpdateTerminalUI();
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Debug.Log("exit page!");
            }
        }
    }
    
    
    
    

    void UpdateTerminalUI()
    {
       terminalUI[progresBarIndex].text = GenerateProgressBar(garbage_bin.GetCurrentObjects());
       terminalUI[roomStatusIndex].text = GenerateStatus(garbage_bin.CheckCompletion() ? "normal" : "lockdown");
    }

    
    void RunPage()
    {
        terminalManager.ClearScreen(0);
        terminalManager.UserInputState(false);
        LoadTitle("garbageUI.txt");
        terminalUI = terminalManager.GetDynamicLines();
        progresBarIndex = terminalUI.FindIndex(line => line.text.Contains("CONTAMINATION LEVEL:"));
        roomStatusIndex = terminalUI.FindIndex(line => line.text.Contains("ROOM STATUS: "));
    }
    string GenerateStatus(string status)
    {
        return $"| ROOM STATUS:      {status}                                ";
    }
    string GenerateProgressBar(int level)
    {
        int total = garbage_bin.GetNumberOfObjects();
        int totalBlocks = 20; // Total number of blocks in the bar
        // ReSharper disable once PossibleLossOfFraction
        float percentage = (float)level / total;
        int filledBlocks = Mathf.RoundToInt(percentage* totalBlocks);
        int emptyBlocks = totalBlocks - filledBlocks;

        // Build the progress bar string
        string progressBar = new string('█', filledBlocks) + new string('░', emptyBlocks);

        // Format the string
        return $"| CONTAMINATION LEVEL: [ {progressBar} ] {(percentage)*100}%";
    }
}
