using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AI_Interperter : BaseInterperter
{

    TerminalManager terminalManager;
    public List<string> code_template;
    private List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }


    public bool print;


    // Start is called before the first frame update
    void Start()
    {
        terminalManager = GetComponent<TerminalManager>();      
        terminalManager.UserInputState(false);  
        print = false;

        terminalManager.NoUserInputLines(LoadTitle("Ship.txt","white",3));

    }

    public override List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split();

        if(args[0] == "help")
        {
         
            response.Add("glad to help!");
            return response;
        }
        else
        {
            response.Add("unrecognized command!");
            return response;
        }
    }


    List<string> LoadTitle(string path, string color, int spacing)
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

        return response;
    }

    string ConvertToSpeech(string s)
    {
        return ColorString("AI-> ", "red") + ColorString(s, "white");
    }
    
    public void PushLines(List<string> msgs, float delay)
    {
        //manage ASCII prints, delay, color, etc

        for(int i=0; i<msgs.Count; i++)
        {
            if (msgs[i].StartsWith("/ASCII "))
            {
                string file = msgs[i].Substring("/ASCII ".Length);
                
                //define programmatically later
                LoadTitle(file, "white", 3);
                msgs.Remove(msgs[i]);
            }
            msgs[i] = ConvertToSpeech(msgs[i]);
        }
        terminalManager.NoUserInputLines(msgs);
    }
}
