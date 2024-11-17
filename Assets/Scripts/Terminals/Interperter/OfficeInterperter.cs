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

        if(args == "help"){}
    }
}
