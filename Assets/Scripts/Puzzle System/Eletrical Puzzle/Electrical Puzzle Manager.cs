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

    [SerializeField] private List<Light> lights;

    private void Start()
    {
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
    }

    private void Update()
    {
        if (tutorial1.CheckCompletion())
        {
            lights[0].enabled = true;
        }
        if (tutorial2.CheckCompletion())
        {
            lights[1].enabled = true;
            lights[2].enabled = true;


        }

    }
}
