using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechRecInterpreter : BaseInterperter
{
    public TerminalManager terminalManager;
    public MicrophoneDetector microphoneDetector;
    [SerializeField] private Interactable interactable;
    private List<TMP_Text> terminalUI;
    public int timeStatusIndex, MicStatusIndex, VoiceCommandIndex;
    public int timeMic= 8;

    List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }
    private bool isListening = false;

    private void Start()
    {
        interactable.enabled = false;
        terminalManager.NoUserInputLines(LoadTitle("micUI.txt", "white", 0)); 
        terminalUI = terminalManager.GetDynamicLines();
        timeStatusIndex = terminalUI.FindIndex(line => line.text.Contains("WAITING "));
        MicStatusIndex = terminalUI.FindIndex(line => line.text.Contains("MIC STATUS: "));
        VoiceCommandIndex = terminalUI.FindIndex(line => line.text.Contains("VOICE COMMAND: "));

        StartCoroutine(WaitForMicrophoneInput());

    }
    private void Update()
    {
        UpdateTerminalUI(); 
    }

    public override List<string> Interpert(string input)
    {
            return response;
    }
    void UpdateTerminalUI()
    {
        terminalUI[timeStatusIndex].text = "║WAITING " + timeMic.ToString();
        terminalUI[MicStatusIndex].text = isListening ? "║ MIC STATUS: ACTIVATED" : "║ MIC STATUS OFF";
        terminalUI[VoiceCommandIndex].text = microphoneDetector.state ? "║ VOICE COMMAND: OPEN DOOR":"║ VOICE COMMAND: NOT RECONIZED";
    }

    private IEnumerator WaitForMicrophoneInput()
    {
        terminalManager.UserInputState(true);
        // Wait for the "V" key to be pressed
        while (!Input.GetKeyDown(KeyCode.V))
        {
            yield return null; // Wait for the next frame
        }
        microphoneDetector.StartRecording();
        isListening = true;

        yield return StartCoroutine(ExtendedListeningWindow(8));

        
        if (microphoneDetector.state)
        {
            interactable.enabled = true;
        }
        else
        {
            //Change for Voice Command not Recognized
            
        }

        isListening = false; 
    }

    private IEnumerator ExtendedListeningWindow(int seconds)
    {

        timeMic = seconds; 
        while (timeMic > 0)
        {
            yield return new WaitForSeconds(1);
            timeMic--; 
        }
    }
}


