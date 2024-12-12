using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeInterperter : BaseInterperter
{
    List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }
    private string correctPassword = "earth";
    [SerializeField] private Interactable door;

    private void Start()
    {
        door.enabled = false;
    }

    public override List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split();

        if (args[0] == "help")
        {
            response.Add("help");
            response.Add("password <correctpassword>");
            return response;
        }
        if (args[0] == "password") 
        {
            if (args.Length > 1 && args[1] == correctPassword)
            {
                OpenDoor(); 
                response.Add("Access granted. Opening Door.");
            }
            else
            {
                response.Add("Incorrect password.");
            }
            return response;
        }
        else
        {
            response.Add("command not recognized");
            return response;
        }
    }

    private void OpenDoor()
    {
       door.enabled = true;
    }
}
