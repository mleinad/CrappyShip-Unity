using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalPuzzleManager : MonoBehaviour
{
    [SerializeField]
    private PuzzleComposite tutorial1;
    [SerializeField]
    private PuzzleComposite tutorial2;
    
    [SerializeField]
    private PuzzleComposite mainPuzzleComposite;


    private void Start()
    {
        EventManager.Instance.OnTurnOnLights(false);
    }

    private void Update()
    {
        if (tutorial1.CheckCompletion())
        {
        }
        if (tutorial2.CheckCompletion())
        {
            
        }

    }
}
