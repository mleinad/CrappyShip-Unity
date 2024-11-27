using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class reactorinterpreter : BaseInterperter,IPuzzleComponent
{
    public TerminalManager terminalManager;
    private List<string> _response = new List<string>();
    [SerializeField]
    Interactable interactable;
    [SerializeField]
    Animator vent;


    private int progresBarIndex;
    private int roomStatusIndex;
    private List<TMP_Text> terminalUI;
    private bool done = false;
    private int currentLevel = 0;
    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public int incrementValue = 15;
    public int decrementValue = 10;
    public int maxLevel = 100;
    private bool isLocked = false;

    private void Start()
    {
        terminalManager.UserInputState(false);
        terminalManager.NoUserInputLines(LoadTitle("reactorUI.txt", "white", 0));
        terminalUI = terminalManager.GetDynamicLines();

        progresBarIndex = terminalUI.FindIndex(line => line.text.Contains("QUANTIC ENERGY LEVEL: "));
        roomStatusIndex = terminalUI.FindIndex(line => line.text.Contains("REACTOR STATUS: "));
    }
    

    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }

   
    private void Update()
    {
        if (!isLocked)
        {
            HandleInput();
        }

            UpdateTerminalUI();
    }
    void HandleInput()
    {
        if (interactable.WasTriggered())
        {
            if (Input.GetKeyDown(upKey))
            {
                currentLevel += incrementValue;

            
                if (currentLevel > maxLevel)
                {
                    currentLevel = 0;
                }
                if(currentLevel == maxLevel)
                {
                    isLocked = true;
                }
            }
            else if (Input.GetKeyDown(downKey))
            {
                currentLevel -= decrementValue;

              
                if (currentLevel < 0)
                {
                    currentLevel = 0;
                }
                if (currentLevel == maxLevel)
                {
                    isLocked = true;
                }
            }
        }
    }

    void UpdateTerminalUI()
    {
        terminalUI[progresBarIndex].text = GenerateProgressBar(currentLevel);

        terminalUI[roomStatusIndex].text = GenerateStatus(done ? "ACTIVATED" : "OFF");
    }
    string GenerateStatus(string status)
    {
        return $"║ ROOM STATUS:      {status}                                ";
    }
    string GenerateProgressBar(int level)
    {
        int total = maxLevel;
        int totalBlocks = 20; // Total number of blocks in the bar
        // ReSharper disable once PossibleLossOfFraction
        float percentage = (float)level / total;
        int filledBlocks = Mathf.RoundToInt(percentage * totalBlocks);
        int emptyBlocks = totalBlocks - filledBlocks;

        // Build the progress bar string
        string progressBar = new string('█', filledBlocks) + new string('░', emptyBlocks);

        if (level == maxLevel)
        {
            done = true;
            vent.SetBool("isDone",true);
        }
        else
        {
            done = false;
        }

        // Format the string
        return $"║ CONTAMINATION LEVEL: [ {progressBar} ] {(percentage * 100):0}%";
        
    }

    public override List<string> Interpert(string input)
    {

        return response;
    }

    public bool CheckCompletion() => done;

    public void ResetPuzzle()
    {
        done = false;
        currentLevel = 0;
        isLocked = false;
        vent.SetBool("isDone", false);
    }
}
