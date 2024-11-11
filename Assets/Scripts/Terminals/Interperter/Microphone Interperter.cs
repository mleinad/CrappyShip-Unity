using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MicrophoneInterperter : MonoBehaviour, Iinterperter
{

    List<string> response = new List<string>();
    TerminalManager terminalManager;
    
    MicrophoneDetector microphoneDetector;
    IPuzzleComponent mic;
    void Start()
    {
        terminalManager = GetComponent<TerminalManager>();
        microphoneDetector = GetComponent<MicrophoneDetector>();
        mic = microphoneDetector.GetComponent<IPuzzleComponent>();
        
    }

    void Update()
    {
        if(mic.CheckCompletion())
        {
            Debug.Log("open door");
        }
    }    
    public List<string> Interpert(string input)
    {
       response.Clear();

        string[] args = input.Split();


        if(args[0]=="open")
        {
            response.Add("Keyboard malfunction, please use voice commands");
            StartCoroutine(ActiveMic());
            return response;
        }
        else 
        {
            response.Add("Command not recognized. Type help for a list of commands");
            return response;
        }
    }

    IEnumerator ActiveMic()
    {
        microphoneDetector.active = true;
        yield return new WaitForSeconds(10);
        microphoneDetector.active = true;
    }
}
