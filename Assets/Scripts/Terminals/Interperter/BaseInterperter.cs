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
    
    protected bool IsValidFile(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return false;

        int lastDotIndex = fileName.LastIndexOf('.');
        if (lastDotIndex == -1 || lastDotIndex == fileName.Length - 1)
        {
            // No extension found or the dot is at the end of the string
            return false;
        }
        
        string extension = fileName.Substring(lastDotIndex + 1).ToLower(); // Extract and normalize the extension
        return fileTypes.Contains(extension); // Check if it's in the valid file types list
    }
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

    protected List<string> LoadTitle(string path, int mode)
    {
        List<string> internalLines = new List<string>();
        
        StreamReader file = new StreamReader(Path.Combine(Application.streamingAssetsPath, path));
        while(!file.EndOfStream)
        {   
            string temp_line = file.ReadLine();
            
            switch (mode)
            {
                case 0:
                    response.Add(temp_line);
                    break;
                case 1:
                    internalLines.Add(temp_line);
                    break;
            }
        }
        file.Close();

        if(mode == 0) return response;
        else return internalLines;
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
