using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeInterperter : MonoBehaviour, Iinterperter
{

    List<string> response = new List<string>();
    public List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split();

        if (args[0] == "help")
        {
            response.Add("help");
            return response;
        }
        if (args[0] == "example")
        {
            response.Add("....");
            return response;
        }
        else
        {
            response.Add("command not recognized");
            return response;
        }
    }
}
