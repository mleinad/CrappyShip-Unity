using System.Collections;
using System.Collections.Generic;
using System.IO;
using MoodMe;
using UnityEngine;

public class FaceIDInterperter : BaseInterperter, IPuzzleComponent
{
    private List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }
    
    bool state = false;
    public List<string> code_template;
    TerminalManager terminalManager;
    
     public float surprised__factor=0.0f;
    public PuzzleComposite puzzleComposite;
    
    public GameObject face_camera;
    public GameObject _keycard;
    public GameObject _faceID;

    IPuzzleComponent keycard;

    [SerializeField]
    private List<Interactable> interactables;

    private int n;
    void Start()
    {
        keycard = _keycard.GetComponent<IPuzzleComponent>();

        terminalManager = GetComponent<TerminalManager>();
        code_template = new List<string>();
        
        foreach (var i in interactables)
        {
            i.enabled = false; 
        }
    }

    public override List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split(); // help ffff ffff 

        if(args[0] == "help" )
        {
            ListEntry("help", "returns a list of commands");
            ListEntry("open", "opens door");
            ListEntry("scan id", "scans user's id card");
            return response;
            
        
        }
        if(args[0]=="open")
        {
            if(puzzleComposite.CheckCompletion())
            {
                response.Add("opening door!");  //add ascii
                Solved();
            }
            else
            {
                if(!keycard.CheckCompletion())
                {
                    response.Add("no keycard found, access denied!");
                }
                else
                {
                    response.Add("facial recognition...");
                    StartCoroutine(ScanFace());
                }

            }


            return response;
        }
        else
        {
            response.Add("Command not recognized. Type help for a list of commands");
            return response;
        }
    }
    
    
    void ListEntry(string a, string b)
    {
        response.Add(ColorString(a, colors["orange"]) + ":" + ColorString(b, colors["yellow"]));
    }


    void Solved()
    {
        foreach (var i in interactables)
        {
           i.enabled = true; 
        }
    }

    public bool CheckCompletion()
    {
       return state;
    }

    public void ResetPuzzle()
    {
        state = false;
    }


    IEnumerator ScanFace()
    {
        face_camera.SetActive(true); 
        
        surprised__factor = EmotionsManager.Emotions.surprised;
        if(surprised__factor>.3f)
        {
            state = true;
        }
        yield return new WaitForSeconds(10);
        face_camera.SetActive(false);

        if(state)
            terminalManager.NoUserInputLines(new List<string>{"access granted!"});
        else terminalManager.NoUserInputLines(new List<string>{"access denied!"});
    }
}
