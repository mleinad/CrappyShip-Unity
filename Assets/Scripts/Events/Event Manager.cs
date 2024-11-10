using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    
    public event Action<int> onTriggerOpenDoor; //future proof for multiple doors
    public event Action <string> onTriggerDebug;
    public event Action <PuzzleComposite> onTriggerSolved;

  
    public event Action <IPuzzleComponent> onAiInteraction;




    private void Awake()
    {
        // Implement Singleton Pattern
        if (Instance == null)
        {
            Instance = this;  // Set this instance as the singleton instance
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // Destroy the extra instance to ensure there is only one
        }

        DontDestroyOnLoad(gameObject);  // Optional: Persist the singleton across scenes
    }



    //test function
    public void OnTriggerOpenDoor()
    {
        onTriggerOpenDoor?.Invoke(1);
    }


    public void OnTriggerDebug(string message)
    {
        onTriggerDebug?.Invoke(message);
    }
    

    public void OnTriggerSolved(PuzzleComposite puzzleComposite)
    {
        onTriggerSolved?.Invoke(puzzleComposite);
    }

    public void OnAiInteraction(IPuzzleComponent puzzleComp)
    {
        onAiInteraction?.Invoke(puzzleComp);
    }
    
}
