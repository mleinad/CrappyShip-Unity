using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Interperter : MonoBehaviour, Iinterperter
{

    TerminalManager terminalManager;

    List<string> response = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        terminalManager = GetComponent<TerminalManager>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<string> Interpert(string input)
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

    

}
