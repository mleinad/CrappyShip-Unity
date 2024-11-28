using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecyclingInterperter : BaseInterperter
{
    private List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }
    
    [SerializeField] GameObject puzzle_component_gameobject;
    PressurePlate garbage_bin;
    TerminalManager terminalManager;
    [Range(0, 100)] public int contaminationLevel = 0;
    [SerializeField] private List<Interactable> interactable;

    
    private Dictionary<string, Action<string[]>> commandHandlers;
    private Dictionary<string, Action> programHandlers;
    
    private Action deferredAction;
    
    
    private int progresBarIndex;
    private int roomStatusIndex;
    private List<TMP_Text> terminalUI;

    private bool page = false;
    
    UIPage recyclingUI;

    void Start()
    {
        terminalManager = GetComponent<TerminalManager>();
        garbage_bin = puzzle_component_gameobject.GetComponent<PressurePlate>();

        foreach (Interactable i in interactable)
        {
            i.enabled = false;
        }
        InitializeHandlers();

        EventManager.Instance.onAiTrigger += CheckSolved;
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
            {"system_ctrl.exe", RunManager },
            {"room_manual.pdf", HandleManual },
            {"last_log.txt",HandleLog },
        };
    }
    private void HandleList(string[] args)
    {
        foreach (string programs in programHandlers.Keys)
        {
            response.Add(programs);
        }
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

    private void HandleManual()
    {
        terminalManager.ClearScreen(0);
        LoadTitle("manual1.txt", 0);
    }
    
    private void HandleLog()
    {
        terminalManager.ClearScreen(0);
        LoadTitle("log1.txt", 0);
    }
    public override List<string> Interpert(string input)
    {
        response.Clear();
        
        deferredAction = null; 
        
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

    private void Update()
    {
        if (page)
        {
            UpdateTerminalUI();
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                recyclingUI.TurnOffPage();
                page = false;   
                LoadCommandLine();
            }
        }
        
        
        
    }


    void LoadCommandLine()
    {
        terminalManager.UserInputState(true);
        
    }
    
    
    #region recycling UI
    void UpdateTerminalUI()
    {
       terminalUI[progresBarIndex].text = GenerateProgressBar(garbage_bin.GetCurrentObjects());
       terminalUI[roomStatusIndex].text = GenerateStatus(garbage_bin.CheckCompletion() ? "NORMAL" : "LOCKDOWN");
    }
    private void RunManager()
    {
        RunPage();
        // Puzzle logic
    }
    void RunPage()
    {
        response.Clear();
        terminalManager.ClearScreen(0);
        terminalManager.UserInputState(false);
        terminalManager.NoUserInputLines(LoadTitle("garbageUI.txt",1));
        //using the new class
        terminalUI = terminalManager.GetDynamicLines();
        recyclingUI = new UIPage(terminalManager.GetDynamicLines());


        progresBarIndex = recyclingUI.GetElementID("CONTAMINATION:");
        roomStatusIndex = recyclingUI.GetElementID("ROOM STATUS:");
        
        page = true;
    }
    string GenerateStatus(string status)
    {
        return $"| <b>ROOM STATUS:</b>    <color=#FF4500> {status}</color> <i>[OVERRIDE REQUIRED]</i>  ";
    }
    string GenerateProgressBar(int level)
    {
        int total = garbage_bin.GetNumberOfObjects();
        int totalBlocks = 20; // Total number of blocks in the bar
        // ReSharper disable once PossibleLossOfFraction
        float percentage = (float)level / total;
        int filledBlocks = Mathf.RoundToInt(percentage* totalBlocks);
        int emptyBlocks = totalBlocks - filledBlocks;

        // Build the progress bar string
        string progressBar = new string('█', filledBlocks) + new string('░', emptyBlocks);

        // Format the string
        return $"| <b>CONTAMINATION:</b>  <color=#FFA500>{(percentage)*100}</color> <color=#00FF00>[ {progressBar} ]</color>           ";
    }
    
    #endregion



    
    #region puzzle

    void CheckSolved(IPuzzleComponent puzzle)
    {
        if (puzzle == garbage_bin)
        {
            Solved();
        }
    }
    void Solved()
    {
        foreach (Interactable i in interactable)
        {
            i.enabled = true;
        }
    }
    
    #endregion
}
