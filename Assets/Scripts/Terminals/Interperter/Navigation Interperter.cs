using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationInterperter : MonoBehaviour, Iinterperter
{
     List<string> response = new List<string>();
    TerminalManager terminalManager;

    IPuzzleComponent superComputer;
    public List<string> Interpert(string input)
    {
        response.Clear();

            string[] args = input.Split();

            if(args[0]=="set destination")
            {
                if(superComputer.CheckCompletion())
                {

                response.Add("Thank you for using the terminal");
                response.Add("---------------------------------");

                return response;
                }
                else 
                {
                response.Add("Something went wrong");

                return response;

                }
            }
            else
            {
                response.Add("Command not recognized. Type help for a list of commands");
                return response;
            }



    }
}
