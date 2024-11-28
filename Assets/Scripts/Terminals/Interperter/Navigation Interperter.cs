using System.Collections.Generic;
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
    public bool page;

    private int powerIndex;
    private int engineIndex;
    private int gyroIndex;
    
    [SerializeField]
    PuzzleComposite powerComponent;
    [SerializeField]
    PuzzleComposite enginesComponent;
    [SerializeField]
    PuzzleComposite gyroComponent;

    private void Start()
    {
        terminalManager = GetComponent<TerminalManager>();
        terminalManager.UserInputState(false);
        terminalManager.NoUserInputLines(LoadTitle("navigationUI.txt", 0));
        page1 = terminalManager.GetDynamicLines();

        powerIndex = page1.FindIndex(line => line.text.Contains("| Power Generator"));
        engineIndex = page1.FindIndex(line => line.text.Contains("| Engines       "));
        gyroIndex = page1.FindIndex(line => line.text.Contains("| Quantum Gyroscope  "));
    }

    public override List<string> Interpert(string input)
    {
            response.Clear();

            string[] args = input.Split();

            return response;


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
        if (powerComponent.CheckCompletion())
        {
            page1[powerIndex].text = GeneratePowerMessage("Operational");
        }
        else
        {
            page1[powerIndex].text = GeneratePowerMessage("ERROR");
        }
        
        if (enginesComponent.CheckCompletion())
        {
            page1[engineIndex].text = GenerateEngineMessage("Online");
        }
        else
        {
            page1[engineIndex].text = GenerateEngineMessage("Offline");
        }
        
        if (gyroComponent.CheckCompletion())
        {
            page1[gyroIndex].text = GenerateGyroMessage("Aligned");
        }
        else
        {
            page1[gyroIndex].text = GenerateGyroMessage("Unaligned");
        }
        
    }

    string GeneratePowerMessage(string message)
    {
        return $"| Power Generator    [\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588---]  87% {message}";
    }

    string GenerateEngineMessage(string message)
    {
        return $"| Engines            [\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588-----]  75% {message}";
    }

    string GenerateGyroMessage(string message)
    {
        return $"| Quantum Gyroscope  [\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2588----]  80% {message}";
    }
    
}




