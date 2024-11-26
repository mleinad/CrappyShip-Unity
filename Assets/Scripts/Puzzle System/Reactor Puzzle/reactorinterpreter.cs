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
        HandleInput();
        UpdateTerminalUI();
    }
    void HandleInput()
    {
        if (interactable.WasTriggered())
        {
            if (currentLevel == 100 && Input.GetKeyDown(upKey)) return;
            if (currentLevel == 0 && Input.GetKeyDown(downKey)) return;

            if (Input.GetKeyDown(upKey))
            {
                currentLevel = Mathf.Clamp(currentLevel + 5, 0, 100);
            }
            else if (Input.GetKeyDown(downKey))
            {
                currentLevel = Mathf.Clamp(currentLevel - 5, 0, 100);
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
        return $"| ROOM STATUS:      {status}                                ";
    }
    string GenerateProgressBar(int level)
    {
        int total = 100;
        int totalBlocks = 20; // Total number of blocks in the bar
        // ReSharper disable once PossibleLossOfFraction
        float percentage = (float)level / total;
        int filledBlocks = Mathf.RoundToInt(percentage * totalBlocks);
        int emptyBlocks = totalBlocks - filledBlocks;

        // Build the progress bar string
        string progressBar = new string('█', filledBlocks) + new string('░', emptyBlocks);

        if (level == 100)
        {
            done = true;
            vent.SetBool("isDone",true);
        }
        else
        {
            done = false;
        }

        // Format the string
        return $"| CONTAMINATION LEVEL: [ {progressBar} ] {(percentage) * 100}%";
        
    }

    public override List<string> Interpert(string input)
    {

        return response;
    }

    public bool CheckCompletion() => done;

    public void ResetPuzzle()
    {
        done = false;
    }
}
