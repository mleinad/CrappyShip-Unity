using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class RecyclingInterperter : MonoBehaviour, Iinterperter
{

    [SerializeField] GameObject puzzle_component_gameobject;
    PressurePlate garbage_bin;
    List<string> response = new List<string>();
    TerminalManager terminalManager;
    [Range(0, 100)] public int contaminationLevel = 0;
    [SerializeField] private List<Interactable> interactable;
    Dictionary<string, string> colors = new Dictionary<string, string>
    {
        { "orange", "#FA4224" },
        { "yellow", "#FDDC5C" },
        { "blue", "#475F94" },
        { "green", "#00ff1b" },
        { "red", "#ff0000" },
        { "white", "#ffffff" }
    };


    private int progresBarIndex;
    private int roomStatusIndex;
    private List<TMP_Text> terminalUI;

    void Start()
    {
        terminalManager = GetComponent<TerminalManager>();
        garbage_bin = puzzle_component_gameobject.GetComponent<PressurePlate>();
        terminalManager.NoUserInputLines(LoadTitle("garbageUI.txt", "white", 3));

        foreach (Interactable i in interactable)
        {
            i.enabled = false;
        }

        terminalUI = terminalManager.GetDynamicLines();
        progresBarIndex = terminalUI.FindIndex(line => line.text.Contains("CONTAMINATION LEVEL:"));
        roomStatusIndex = terminalUI.FindIndex(line => line.text.Contains("ROOM STATUS: "));
    }

    public List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split();

        if (args[0] == "help")
        {

            ListEntry("help", "returns a list of commands");
            ListEntry("open", "opens door");
            return response;

        }

        if (args[0] == "open")
        {

            if (!garbage_bin.CheckCompletion())
            {
                response.Add("checking room contamination...");
                //LoadTitle("","",2);
                response.Add(BoldString("Unable to open door, contamination risk too high!"));
            }
            else
            {
                response.Add("Room unlocked...");
                Solved();
            }

            return response;
        }
        else
        {
            response.Add("Command not recognized. Type help for a list of commands");
            return response;
        }
    }

    
    #region style

    public string BoldString(string s)
    {
        return "<b>" + s + "</b>";
    }

    public string HighlightString(string s, string color)
    {
        string leftTag = "<mark=" + color + ">";
        string rightTag = "</mark>";

        return leftTag + s + rightTag;
    }

    public string ColorString(string s, string color)
    {
        string leftTag = "<color=" + color + ">";
        string rightTag = "</color>";

        return leftTag + s + rightTag;
    }

    #endregion

    void ListEntry(string a, string b)
    {
        response.Add(ColorString(a, colors["orange"]) + ":" + ColorString(b, colors["yellow"]));
    }

    List<string> LoadTitle(string path, string color, int spacing)
    {
        StreamReader file = new StreamReader(Path.Combine(Application.streamingAssetsPath, path));
        for (int i = 0; i < spacing; i++)
        {
            response.Add("");
        }

        while (!file.EndOfStream)
        {
            string temp_line = file.ReadLine();
            if (color == string.Empty)
            {
                response.Add(temp_line);
            }
            else
            {
                response.Add(ColorString(temp_line, colors[color]));
            }
        }

        for (int i = 0; i < spacing; i++)
        {
            response.Add("");
        }

        file.Close();

        return response;
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
        UpdateTerminalUI();
    }
    
    
    
    

    void UpdateTerminalUI()
    {
        terminalUI[progresBarIndex].text = GenerateProgressBar(garbage_bin.GetCurrentObjects());

        terminalUI[roomStatusIndex].text = GenerateStatus(garbage_bin.CheckCompletion() ? "normal" : "lockdown");
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
