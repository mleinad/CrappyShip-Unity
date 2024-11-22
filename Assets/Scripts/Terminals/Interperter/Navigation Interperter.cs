using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class NavigationInterperter : MonoBehaviour, Iinterperter
{
     List<string> response = new List<string>();
    TerminalManager terminalManager;
    Dictionary<string, string> colors = new Dictionary<string, string>
    {
        { "orange", "#FA4224" },
        { "yellow", "#FDDC5C" },
        { "blue", "#475F94" },
        { "green", "#00ff1b" },
        { "red", "#ff0000" },
        { "white", "#ffffff" }
    };
    IPuzzleComponent superComputer;
    public List<TMP_Text> page1;
    public List<TMP_Text> page2;
    public bool page;
    private void Start()
    {
        terminalManager = GetComponent<TerminalManager>();
        
        terminalManager.NoUserInputLines(LoadTitle("navigationUI.txt", "white", 1)); 
        page1 = terminalManager.GetDynamicLines();
        response.Clear();
        terminalManager.NoUserInputLines(LoadTitle("garbageUI.txt", "white", 1));   //change to add more tabs, implement with page changing system
        page2 = terminalManager.GetDynamicLines();
        
        foreach (var line in page1)
        {
            page2.Remove(line); // Remove page1 lines from page2
        }
    }

    public List<string> Interpert(string input)
    {
            response.Clear();

            string[] args = input.Split();

            if(args[0]=="set destination")
            {
                if(superComputer.CheckCompletion())
                {

                response.Add("Thank you for using the terminal");
                response.Add("---------------------------------");

                return response;
                }
                else 
                {
                response.Add("Something went wrong");

                return response;

                }
            }
            else
            {
                response.Add("Command not recognized. Type help for a list of commands");
                return response;
            }



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


    private void EnableUI(List<TMP_Text> layout, bool enable)
    {
        foreach (var line in layout)
        {
            line.transform.parent.gameObject.SetActive(enable);
        }
    }

    private void Update()
    {
        if (page)
        {
            EnableUI(page1, true);
            EnableUI(page2, false);
        }
        else
        {
            EnableUI(page1, false);
            EnableUI(page2, true);
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
}




