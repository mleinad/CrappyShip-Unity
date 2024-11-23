using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EletricalInterperter : BaseInterperter
{
    
    private List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }
    
    [SerializeField] List<Fuse> fuses = new List<Fuse>();
    [SerializeField] PuzzleComposite puzzleComposite;

    public List<string> code_template;
    TerminalManager terminalManager;
    
    public Animator doorAnimator;
    
    public override List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split();

        if (args[0] == "help")
        {

            ListEntry("help", "returns a list of commands");
            ListEntry("open", "opens door");
            ListEntry("restore", "restore the ship's eletrical system");
            return response;

        }
        if (args[0] == "open")
        {
            if (puzzleComposite.CheckCompletion())
            {
                response.Add("opening door...");
                Open();
            }
            else
            {
                response.Add("energy level too low");
            }
            return response;
        }

        if (args[0] == "restore")
        {
            response.Add("restoring system...");
            return response;
        }
        else
        {
            response.Add("Command not recognized. Type help for a list of commands");
            return response;
        }
    }

    void Open()
    {
        doorAnimator.SetBool("IsOpen", true);
    }
    
    

    void ListEntry(string a, string b)
    {
        response.Add(ColorString(a, colors["orange"]) + ":" + ColorString(b, colors["yellow"]));
    }
    
}
