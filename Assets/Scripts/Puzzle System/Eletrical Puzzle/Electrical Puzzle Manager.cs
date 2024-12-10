using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalPuzzleManager : MonoBehaviour
{
    [SerializeField]
    private PuzzleComposite tutorialPuzzle;
    
    [SerializeField]
    private PuzzleComposite mainPuzzleComposite;

    [SerializeField]
    private PuzzleComposite puzzleComposite1;
    [SerializeField]
    private PuzzleComposite puzzleComposite2;
    
    [SerializeField]
    List<Light> redLights1;
    [SerializeField]
    List<Light> redLights2;
    [SerializeField]
    List<Light> redLights3;
            
    
    
    
    
    private void Start()
    {
        EnableLights(redLights1, true);
        EnableLights(redLights2, false);
        EnableLights(redLights3, false);
        EventManager.Instance.OnTurnOnLights(false);

        EventManager.Instance.onTriggerSolved += TurnOnLights;

    }

    private void Update()
    {
        
    }
    
    private void EnableLights(List<Light> lights, bool enable)
    {
        foreach (var light in lights)
        {
            light.enabled = enable;
        }
    }

    void TurnOnLights(IPuzzleComponent puzzleComponent)
    {
        if (ReferenceEquals(puzzleComponent, puzzleComposite1))
        {
            EnableLights(redLights2, true);
            Debug.Log("Turned on Red Lights");
        }
        if (ReferenceEquals(puzzleComponent, tutorialPuzzle))
        {
            EnableLights(redLights3, true);
        }

        if (ReferenceEquals(puzzleComponent, mainPuzzleComposite))
        {
            EnableLights(redLights1, false);
            EnableLights(redLights2, false);
            EnableLights(redLights3, false);
            EventManager.Instance.OnTurnOnLights(true);
            Player.Instance.EnablePlayerLight(false);
            EventManager.Instance.OnAiTrigger(mainPuzzleComposite);
        }
    }
}
