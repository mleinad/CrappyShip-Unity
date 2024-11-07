using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FaceIDInterperter : MonoBehaviour, Iinterperter
{
       Dictionary<string, string> colors = new Dictionary<string, string>{
    {"orange", "#FA4224"},
    {"yellow", "#FDDC5C"},
    {"blue", "#475F94"},
    {"green", "00ff1b"},
    {"red", "ff0000"},
    {"white", "ffffff"}
    };


    public List<string> code_template;
    List<string> response = new List<string>();
    TerminalManager terminalManager;
    
    
    public PuzzleComposite puzzleComposite;
    
    
    public GameObject _keycard;
    public GameObject _faceID;

    IPuzzleComponent keycard;
    IPuzzleComponent faceID;

    void Start()
    {
        keycard = _keycard.GetComponent<IPuzzleComponent>();
        faceID = _faceID.GetComponent<IPuzzleComponent>();

        terminalManager = GetComponent<TerminalManager>();
        code_template = new List<string>();
    }

    public List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split();

        if(args[0] == "help")
        {
            ListEntry("help", "returns a list of commands");
            ListEntry("open", "opens door");
            ListEntry("scan id", "reads user's id card");
            return response;
        
        }
        if(args[0]=="open")
        {
            if(puzzleComposite.CheckCompletion())
            {
                response.Add("opening door!");  //add ascii
            }
            else
            {
                if(!keycard.CheckCompletion())
                {
                    response.Add("no keycard found, access denied!");
                }
                else
                {
                    if(faceID.CheckCompletion())
                    {
                        response.Add("facial recognition failed, access denied!");
                    }
                }


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
        string rightTag ="</color>";

        return leftTag + s + rightTag;
    }

    #endregion
    void ListEntry(string a, string b)
    {
        response.Add(ColorString(a, colors["orange"]) + ":" + ColorString(b, colors["yellow"]));
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
            string temp_line = file.ReadLine();
            code_template.Add(temp_line);    
            if(color == string.Empty) 
            {
                response.Add(temp_line);
            }
            else 
            {
                response.Add(ColorString(temp_line, colors[color]));
            }
        }
        for(int i=0; i< spacing; i++)
        {
            response.Add("");
        }
        file.Close();
    }

}
