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
        { "green", "#00FF1B" },
        { "red", "#FF0000" },
        { "white", "#FFFFFF" },
        { "cyan", "#00FFFF" },
        { "purple", "#8A2BE2" },
        { "pink", "#FF69B4" },
        { "grey", "#808080" },
        { "black", "#000000" },
        { "lightblue", "#ADD8E6" },
        { "lime", "#32CD32" },
        { "gold", "#FFD700" },
        { "darkred", "#8B0000" },
        { "teal", "#008080" },
        { "navy", "#000080" },
        { "silver", "#C0C0C0" },
        { "magenta", "#FF00FF" },
        { "darkgreen", "#006400" }
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

    protected string BoldString(string s)
    {
        return "<b>" + s + "</b>";
    }

    protected string HighlightString(string s, string color)
    {
        string leftTag = "<mark=" + color + ">";
        string rightTag = "</mark>";

        return leftTag + s + rightTag;
    }

    protected string ColorString(string s, string color)
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

    protected void ListFiles(string location, List<string> files)
    {
        response.Add(ColorString(location + ": ", colors["lime"]));
        foreach (var file in files)
        {
            response.Add($"     {file}");
        }
    }

    protected void ListHiddenFiles(string location, List<string> files, List<string> hiddenFiles)
    {
        response.Add(ColorString(location + ": ", colors["darkgreen"]));
        foreach (var file in files)
        {
            response.Add($"     -{file}");
        }
        foreach (var file in hiddenFiles)
        {
            response.Add($"         -{HidenFile(file)}");
        }
    }

    protected string HidenFile(string file)
    {
        return BoldString(ColorString(file, colors["darkred"]));
    }
}
