using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using QFSW.QC.Suggestors;
using Unity.VisualScripting;
using UnityEngine;

public class CodingInterperter : MonoBehaviour, Iinterperter
{
    Dictionary<string, string> colors = new Dictionary<string, string>{
    {"orange", "#FA4224"},
    {"yellow", "#FDDC5C"},
    {"blue", "#475F94"}
    };

    List<string> response = new List<string>();
    TerminalManager terminalManager;


    bool coding = false;
    void Start()
    {
        terminalManager = GetComponent<TerminalManager>();
    }
    public List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split();


        if(args[0]=="code")
        {
            Code();
            return response;
        }
        if(args[0]=="ascii")
        {
            LoadTitle("ASCII.txt","blue", 2);
            return response;
        }
        if(args[0] == "help")
        {
            ListEntry("help", "returns a list of commands");
            ListEntry("stop", "pauses the game");
            ListEntry("run", "resumes the game");
            ListEntry("four", "bla bla bla");
            
            return response;
        }
        if(args[0]=="boop")
        {
            response.Add("Thank you for using the terminal");
            response.Add("---------------------------------");
            return response;
        }
        else
        {
            response.Add("Command not recognized. Type help for a list of commands");

            return response;
        }
    }

    public string ColorString(string s, string color)
    {
        string leftTag = "<color=" + color + ">";
        string rightTag ="</color>";

        return leftTag + s + rightTag;
    }

    void ListEntry(string a, string b)
    {
        response.Add(ColorString(a, colors["orange"]) + ":" + ColorString(b, colors["yellow"]));
    }

    void CorrectLetter(string a, string b)
    {

    }
    void LoadTitle(string path, string color, int spacing)
    {
        StreamReader file = new StreamReader(Path.Combine(Application.streamingAssetsPath, path));
        for(int i =0; i< spacing; i++)
        {
            response.Add("");
        }
        while(!file.EndOfStream)
        {
            response.Add(ColorString(file.ReadLine(), colors[color]));
        }
        for(int i=0; i< spacing; i++)
        {
            response.Add("");
        }
        file.Close();
    }


    void Code()
    {
        LoadTitle("Code.txt", "orange", 2);
        terminalManager.UserInputState(false);
        coding = true;
    }




    
    public void Update()
    {
        if(coding)
        {
          //get any key down here
        }
    }
}
