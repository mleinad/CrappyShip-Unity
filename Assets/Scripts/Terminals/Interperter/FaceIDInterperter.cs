using System;
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

    keycard keycard;

    
    private Dictionary<string, Action<string[]>> commandHandlers;
    private Dictionary<string, Action> programHandlers;
    
    
    
    
    
    
    [SerializeField]
    private List<Interactable> interactables;

    private int n;
    void Start()
    {
        keycard = _keycard.GetComponent<keycard>();

        terminalManager = GetComponent<TerminalManager>();
        code_template = new List<string>();
        
        foreach (var i in interactables)
        {
            i.enabled = false; 
        }
        EventManager.Instance.onTriggerSolved+=OnTriggerSolved;
        InitializeHandlers();
    }

    private void OnTriggerSolved(IPuzzleComponent obj)
    {
        if (obj == puzzleComposite)
        {
            Solved();
        }
    }

    private void InitializeHandlers()
    {
        commandHandlers = new Dictionary<string, Action<string[]>>
        {
            { "help", HandleHelp },
            { "run", HandleRun },
            { "install", args => response.Add("Install functionality is not implemented.") },
            { "delete", args => response.Add("Delete functionality is not implemented.") },
            { "storage", HandleList}
        };

        programHandlers = new Dictionary<string, Action>
        {
            {"id_verify.exe", ScanID },
            {"id_photo.png", () => response.Add("Diary program... beep boop bzzzz") },
        };
    }
    
    private void HandleHelp(string[] args)
    {
        ListEntry("run", "run a program from the terminal");
        ListEntry("storage", "lists installed programs");
        ListEntry("install", "install a new program");
    }
    private void HandleRun(string[] args)
    {
        if (args.Length < 2)
        {
            response.Add("Please specify a program to run.");
            return;
        }

        string programName = args[1];
        if (programHandlers.ContainsKey(programName))
        {
            programHandlers[programName]();
        }
        else
        {
            response.Add("Executable not found.");
        }
    }
    private void HandleList(string[] args)
    {
        foreach (string programs in programHandlers.Keys)
        {
            response.Add(programs);
        }
    }
    
    public override List<string> Interpert(string input)
    {
        
        response.Clear();
        
        input = input.ToLower();
        string[] args = input.Split();

        if (args.Length > 0 && commandHandlers.ContainsKey(args[0]))
        {
            commandHandlers[args[0]](args);
        }
        else
        {
            response.Add("Command not recognized.");
        }
        
        return response;
    }
    
    
    void ListEntry(string a, string b)
    {
        response.Add(ColorString(a, colors["orange"]) + ":" + ColorString(b, colors["yellow"]));
    }

    void ScanID()
    {
        if (keycard.CheckCompletion())
        {
            keycard.MoveToScaner();
            response.Add("adicional security required");
            response.Add("starting facial recognition process...");
            StartCoroutine(ScanFace());
        }
        else
        {
            response.Add("keycard not recognized.");
        }
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

    private int numberOfAttempts = 0;
    IEnumerator ScanFace()
    {
        numberOfAttempts++;
        yield return new WaitForSeconds(0.5f);
        face_camera.SetActive(true); 
        
        surprised__factor = EmotionsManager.Emotions.surprised;
        if(surprised__factor>.3f||numberOfAttempts>=3)
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
