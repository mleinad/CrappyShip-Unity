using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzInterpreter : BaseInterperter
{
    private List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }

    public override List<string> Interpert(string input)
    {

        response.Clear();

        string[] args = input.Split();



        if (args[0] == "help")
        {
            response.Add("Solve a quizz so you can advance to the next room");
            response.Add("Type <quizz.exe> to open the quizz program");
            return response;
        }
        if (args[0]== "quizz.exe")
        {
            response.Add("How many noodle boxes are in this room");
            
            return response;
        }
        if (args[0] == "6")
        {
            response.Add("Correct!");
            return response;
        }

       
        else
        {
            response.Add("Type <help> for more information");
            return response;

        }


    }
}

