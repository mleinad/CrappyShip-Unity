using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingInterperter : MonoBehaviour
{
    
    
    List<string> response = new List<string>();
    TerminalManager terminalManager;
    Dictionary<string, string> colors = new Dictionary<string, string>{
    {"orange", "#FA4224"},
    {"yellow", "#FDDC5C"},
    {"blue", "#475F94"},
    {"green", "#00ff1b"},
    {"red", "#ff0000"},
    {"white", "#ffffff"}
    };

    public List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split();


        if(args[0]=="code")
        {

            return response;

        }
        if(args[0]=="ascii")
        {
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
}
