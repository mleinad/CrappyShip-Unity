using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class BaseInterperter: MonoBehaviour
{
    protected Dictionary<string, string> colors = new Dictionary<string, string>
    {
        { "orange", "#FA4224" },
        { "yellow", "#FDDC5C" },
        { "blue", "#475F94" },
        { "green", "#00ff1b" },
        { "red", "#ff0000" },
        { "white", "#ffffff" }
    };

    protected List<string> fileTypes = new List<string>
    {
        "exe",
        "cpp",
        "cs",
        "py"
    };
    public abstract List<string> response { get; set; }

    public abstract List<string> Interpert(string input);
    
    
    protected  List<string> LoadTitle(string path, string color, int spacing)
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

    protected List<string> LoadTitle(string path)
    {
        StreamReader file = new StreamReader(Path.Combine(Application.streamingAssetsPath, path));
        while(!file.EndOfStream)
        {   
            string temp_line = file.ReadLine();
            
            response.Add(temp_line);
        }
        file.Close();

        return response;
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
    
   protected void ListEntry(string a, string b)
    {
        response.Add(ColorString(a, colors["orange"]) + ":" + ColorString(b, colors["yellow"]));
    }
}
