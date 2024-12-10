using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    
    public event Action<Interactable> onTriggerInteraction;
    public event Action <IPuzzleComponent> onTriggerSolved;
    public event Action <IPuzzleComponent> onAiTrigger;
    public event Action <bool> onTurnOnLights;
    
    public event Action <IPuzzleComponent> onHintRequest;


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


    public void OnTriggerInteraction(Interactable obj)
    {
        onTriggerInteraction?.Invoke(obj);
    }

    public void OnTurnOnLights(bool state)
    {
        onTurnOnLights?.Invoke(state);
    }

    public void OnTriggerSolved(IPuzzleComponent puzzleComp)
    {
        onTriggerSolved?.Invoke(puzzleComp);
    }

    public void OnAiTrigger(IPuzzleComponent puzzleComp)
    {
        onAiTrigger?.Invoke(puzzleComp);
    }

    public void OnHintRequest(IPuzzleComponent puzzleComp)
    {
        onHintRequest?.Invoke(puzzleComp);
    }
    
}
