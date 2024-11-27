using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CodingInterperter : BaseInterperter, IPuzzleComponent
{

    private List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }

    private TerminalManager terminalManager;
    public List<string> code_template;

    [SerializeField] private Cable cable;
    [SerializeField] private Interactable interactable;
    
    private bool coding = false;
    private bool locked = true;
    private bool state;

    [SerializeField] private List<BackgroundInterperter> otherMonitors;

    public string terminalPassword;
    public float codingTimer;
    
    
    //core
    private Dictionary<string, Action<string[]>> commandHandlers;
    private Dictionary<string, Action> programHandlers;
    
    private Dictionary<string, Action> filesHandlers;
   
    //after unlock
    private Dictionary<string, Action<string[]>> unlockedCommandHandlers;
    
    //hidden files
    private Dictionary<string, Action> hiddenProgramHandlers;
    private Dictionary<string, Action> hiddenFilesHandlers;
   
    private Dictionary<string, string> commandDescriptions;

    private float timer;
    
    private void Start()
    {
        terminalManager = GetComponent<TerminalManager>();
        code_template = new List<string>();
        state = false;
        interactable.enabled = false;
        
        InitializeHandlers();
    }

    
    private void InitializeHandlers()
    {
        commandHandlers = new Dictionary<string, Action<string[]>>
        {
            { "help", HandleHelp },
            {"unlock", HandleUnlock}
        };
        
        unlockedCommandHandlers = new Dictionary<string, Action<string[]>>
        {
            { "run", HandleRun },
            { "install", args => response.Add("Install functionality is not implemented.") },
            { "delete", HandleDelete },
            { "storage", HandleList},
            {"//.storage", HandleHiddenList},
            {"//.override", HandleOverride}
        };

        programHandlers = new Dictionary<string, Action>
        {
            {"system_ctrl.exe", ()=> response.Add("Decrypter program... beep boop bzzzz")  },
          //  {"system_override.exe", () => response.Add("Decrypter program... beep boop bzzzz") },
          //  {"persona_logs.exe", () => response.Add("Diary program... beep boop bzzzz") }
            
        };

        filesHandlers = new Dictionary<string, Action>()
        {
            { "ship_logs.txt", () => response.Add("Ship logs.txt") },
            { "diagnostics_report.txt", () => response.Add("Diagnostics report.txt") },
            { "crew_manifest.txt", () => response.Add("Crew manifest.txt") },
            { "override_docs.txt", () => response.Add("Crew manifest.txt") }

        };
      
        hiddenProgramHandlers = new Dictionary<string, Action>
        {
            {"hidden.exe", ()=> response.Add("Decrypter program... beep boop bzzzz") },
            {"hidden2.exe", () => response.Add("Decrypter program... beep boop bzzzz") },
            {"hidden3.exe", () => response.Add("Diary program... beep boop bzzzz") },
        };

        hiddenFilesHandlers = new Dictionary<string, Action>()
        {
            { "secret_file.txt", () => response.Add("secret file is opened!") }
        };
        
        commandDescriptions = new Dictionary<string, string>
        {
            { "help", "Lists all available commands." },
            { "unlock", "Unlocks additional commands using a password." },
            { "run", "Runs a specified program, e.g., >run program.exe" },
            { "install", "Installs a new program (functionality not implemented)." },
            { "delete", "Deletes a program or file from storage (functionality not implemented)." },
            { "storage", "Lists all programs and files in storage." }
        };
    }


    #region Handlers

    void HandleHelp(string[] args)
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
    void HandleRun(string[] args)
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

    void HandleList(string[] args)
    {
        ListFiles("Programs", programHandlers.Keys.ToList());
        ListFiles("Downloads", new List<string>());
        ListFiles("Files", filesHandlers.Keys.ToList());
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
    
    void HandleUnlock(string[] args)
    {
        if (args.Length < 2)
        {
            response.Add("No password provided.");
            return;
        }

        string password = args[1];
        if (password == terminalPassword)
        {
            commandHandlers.AddRange(unlockedCommandHandlers);
            commandHandlers.Remove("unlock");
        }
        else
        {
            response.Add("Incorrect password!");
        }
        
    }
    
    void HandleDelete(string[] args)
    {
        if (args.Length < 2)
        {
            response.Add("Please specify a program or file to delete.");
            return;
        }

        string itemName = args[1];

        if (filesHandlers.ContainsKey(itemName))
        {
            filesHandlers.Remove(itemName);
            response.Add($"File '{itemName}' deleted successfully.");
        }
        else
        {
            response.Add("File not found.");
        }
    }

    void HandleOverride(string[] args)
    {
        if (args.Length < 2)
        {
            response.Add("Admin password required, please provide a password after //override command.");
            return;
        }

        string password = args[1];
        if (password == "5678")
        {
            Code();
        }
        else
        {
            response.Add("Incorrect password!, override failed.");
        }
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

    
    private void LoadTitle(string path, string color, int spacing)
    {
        StreamReader file = new StreamReader(Path.Combine(Application.streamingAssetsPath, path));
        for(int i =0; i< spacing; i++)
        {
            response.Add("");
        }
        while(!file.EndOfStream)
        {   
            string temp_line = file.ReadLine();
            code_template.Add(temp_line);    
            if(color == string.Empty) 
            {
                response.Add(temp_line);
            }
            else 
            {
                response.Add(ColorString(temp_line, colors[color]));
            }
        }
        for(int i=0; i< spacing; i++)
        {
            response.Add("");
        }
        file.Close();
    }


#region Typing Game
    
    private int lineIndex = 0;
    public string currentString;
    public string remainingString;


    private void Code()
    {

        foreach (var monitor in otherMonitors)
        {
            monitor.state = 1;
        }
        terminalManager.ClearScreen(0);
        
        LoadTitle("Code.txt", "white", 0);
        coding = true;

        remainingString = code_template[lineIndex];
        terminalManager.UserInputState(false);

        StartCoroutine(Timer());
    }

 
    public void Update()
    {
        if(cable.GetSignal()>0)
        {
            interactable.enabled = true;
        }
        else interactable.enabled = false;


        if(coding)
        {   
            if(terminalManager.GetDynamicLines().Count>0)
            {
                
                if(remainingString == string.Empty)
                {
                    if(Input.GetKeyDown(KeyCode.Return))
                    {
                        lineIndex++;
                        remainingString = code_template[lineIndex];
                    }
                }
                
                CheckInput();

            
            }
        }
    }
    private void CheckInput()
    {
        if(Input.anyKeyDown)
        {
            string input = Input.inputString;
            if(input.Length==1)
            {
                EnterLetter(input);
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
       return remainingString.IndexOf(letter) == 0;
    }

    private void EnterLetter(string letter)
    {
        char expectedLetter = remainingString[0];

        if(IsCorrectLetter(letter))
        {   
    
            currentString += HighlightString(letter, "blue");
             RemoveLetter();
            terminalManager.GetDynamicLines()[lineIndex].text = currentString + remainingString;

            if(isLineComplete())
            {
                lineIndex ++;   //moves to next line
                if (lineIndex < code_template.Count)
                {
                    // Set up for the next line
                    //SetRemainingWord(terminalManager.dynamic_lines[lineIndex].text);
                    currentString = ""; // Reset `currentString` for the new line
                    remainingString = code_template[lineIndex];
                }
                else
                {
                    coding = false; // Stop coding if all lines are complete
                    Debug.Log("Reached the end of the program.");                    
                    ResetPuzzle();
                    StopCoroutine(Timer());
                    

                    
                    foreach (var monitor in otherMonitors)
                    {
                        monitor.state = 0;
                    }
                }
            }
        }
        else
        {   
            terminalManager.GetDynamicLines()[lineIndex].text =  currentString+ HighlightString(expectedLetter.ToString(), "red") + remainingString;
        }
    }
    private void RemoveLetter()
    {
        string new_string = remainingString.Remove(0,1);
        SetRemainingWord(new_string);
    }

    private void SetRemainingWord(string line)
    {
        remainingString = line;
    }

    private bool isLineComplete()
    {
        return remainingString.Length == 0;
    }

    public bool CheckCompletion()
    {
        return state;
    }

    public void ResetPuzzle()
    {
        state=false;
    }
    #endregion

    private IEnumerator Timer()
    {
        
        float remainingTime = codingTimer;

        while (remainingTime > 0)
        {
            
            foreach (var monitor in otherMonitors)
            {
                monitor.time = remainingTime;
            }

            yield return new WaitForSeconds(0.1f); // Update every 0.1 seconds (adjust as needed)
            remainingTime -= 0.1f; // Decrease remaining time
        }

        // When the timer ends
        foreach (var line in terminalManager.GetDynamicLines())
        {
            line.text = BoldString(ColorString("!!!!!!!!!!!____I WON____!!!!!!!!!!!", "red"));
            
        }
        foreach (var line in otherMonitors.SelectMany(monitor => terminalManager.GetDynamicLines()))
        {
            line.text = BoldString(ColorString("!!!!!!!!!!!____I WON____!!!!!!!!!!!", "red"));
        }
    
    }
}
