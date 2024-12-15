using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    //core
    private Dictionary<string, Action<string[]>> commandHandlers;
    private Dictionary<string, Action> programHandlers;
    
    private Dictionary<string, Action> filesHandlers;
    
    //hidden files
    private Dictionary<string, Action> hiddenProgramHandlers;
    private Dictionary<string, Action> hiddenFilesHandlers;
   
    //description
    private Dictionary<string, string> commandDescriptions;

    public SerializedDictionary<string, Texture> imageDictionary;
    
    private int progresBarIndex;
    private int roomStatusIndex;
    private List<TMP_Text> terminalUI;

    private bool page = false;
    
    public GameObject mediaLine;
    public GameObject ASCIICam;
    
    
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
            { "storage", HandleList},
            {"//.storage", HandleHiddenList}
        };

        programHandlers = new Dictionary<string, Action>
        {
            {"system_ctrl.exe", RunManager },
            {"room_manual.pdf", HandleManual },
        };
        
        filesHandlers = new Dictionary<string, Action>()
        {
            { "ship_logs.txt", () => response.Add("Ship logs.txt") },
            { "diagnostics_report.txt", () => response.Add("Diagnostics report.txt") },
            { "crew_manifest.txt", () => response.Add("Crew manifest.txt") },
            { "override_docs.txt", () => response.Add("Crew manifest.txt") },
            { "last_log.txt",HandleLog },
            
        };
        
        hiddenProgramHandlers = new Dictionary<string, Action>
        {
            {"hidden.exe", ()=> response.Add("Decrypter program... beep boop bzzzz") },
            {"hidden2.exe", () => response.Add("Decrypter program... beep boop bzzzz") },
            {"hidden3.exe", () => response.Add("Diary program... beep boop bzzzz") },
        };

        hiddenFilesHandlers = new Dictionary<string, Action>()
        {
            { "hidden_text_file.txt", () => response.Add("secret stuff...!") }
        };

        
        commandDescriptions = new Dictionary<string, string>
        {
            { "help", "Lists all available commands." },
            { "unlock", "Unlocks additional commands using a password." },
            { "run", "Runs a specified program, e.g.->run program.exe" },
            { "install", "Installs a new program (functionality not implemented)." },
            { "delete", "Deletes a program or file from storage (functionality not implemented)." },
            { "storage", "Lists all programs and files in storage." }
        };
        
        
    }
    
    


    #region Handlers
    private void HandleList(string[] args)
    {
        ListFiles("Programs", programHandlers.Keys.ToList());
        ListFiles("Downloads", new List<string>());
        ListFiles("Files", filesHandlers.Keys.ToList());
    }
    private void HandleHelp(string[] args)
    {
        foreach (var command in commandHandlers)
        {   
            if(command.Key.Contains("//")) continue;    //ignore hidden commands

            if (commandDescriptions.TryGetValue(command.Key, out var description))  //not yet implemented descriptions
            { 
                ListEntry(command.Key, description);
            }
        }
    }
    private void HandleRun(string[] args)
    {
        if (args.Length < 2)
        {
            response.Add("Please specify a program to run.");
            return;
        }
        
        string itemName = args[1];

        if (programHandlers.ContainsKey(itemName))
        {
            programHandlers[itemName]();
        }
        else if (filesHandlers.ContainsKey(itemName))
        {
            filesHandlers[itemName]();
        }
        else
        {
            response.Add("Executable or file not found.");
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
    void HandleHiddenList(string[] args)
    {
        

        ListHiddenFiles("Programs", programHandlers.Keys.ToList(), hiddenProgramHandlers.Keys.ToList());
        ListHiddenFiles("Downloads", new List<string>(),new List<string>());
        ListHiddenFiles("Files", filesHandlers.Keys.ToList(), hiddenFilesHandlers.Keys.ToList());

        //needs to attach to program handler to recognize command
        programHandlers.AddRange(hiddenProgramHandlers);
        filesHandlers.AddRange(hiddenFilesHandlers);
        
        //removes files and programs from list again
        foreach (var program in hiddenProgramHandlers.Keys)
        {
            programHandlers.Remove(program);
        }
        foreach (var file in hiddenFilesHandlers.Keys)
        {
            filesHandlers.Remove(file);
        }
    }

    void HandleImage()
    {
        terminalManager.LoadImage(imageDictionary[""]);
    }
    #endregion
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
