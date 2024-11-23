using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class NavigationInterperter : BaseInterperter
{
    private List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }
    TerminalManager terminalManager;

    IPuzzleComponent superComputer;
    public List<TMP_Text> page1;
    public List<TMP_Text> page2;
    public bool page;
    private void Start()
    {
        terminalManager = GetComponent<TerminalManager>();
        
        terminalManager.NoUserInputLines(LoadTitle("navigationUI.txt", "white", 1)); 
        page1 = terminalManager.GetDynamicLines();
        response.Clear();
        terminalManager.NoUserInputLines(LoadTitle("garbageUI.txt", "white", 1));   //change to add more tabs, implement with page changing system
        page2 = terminalManager.GetDynamicLines();
        
        foreach (var line in page1)
        {
            page2.Remove(line); // Remove page1 lines from page2
        }
    }

    public override List<string> Interpert(string input)
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
    

    private void EnableUI(List<TMP_Text> layout, bool enable)
    {
        foreach (var line in layout)
        {
            line.transform.parent.gameObject.SetActive(enable);
        }
    }

    private void Update()
    {
        if (page)
        {
            EnableUI(page1, true);
            EnableUI(page2, false);
        }
        else
        {
            EnableUI(page1, false);
            EnableUI(page2, true);
        }
    }

    
}




