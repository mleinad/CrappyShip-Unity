using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeLasersInterpreter : BaseInterperter
{
    List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }
    private string correctCode = "7591";
    

    private void Start()
    {
        
    }

    public override List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split();

        if (args[0] == "help")
        {
            response.Add("help");
            response.Add("code <correctcode>");
            return response;
        }
        if (args[0] == "code") 
        {
            if (args.Length > 1 && args[1] == correctCode)
            {
                response.Add("Access granted. Lasers on.");
            }
            else
            {
                response.Add("Incorrect code.");
            }
            return response;
        }
        else
        {
            response.Add("command not recognized");
            return response;
        }
    }

    
}
