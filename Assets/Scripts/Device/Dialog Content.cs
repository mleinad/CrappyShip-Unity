using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogContent : MonoBehaviour
{
    public List<string> dialog;

    public bool isRepeated = false;
    int repCount;
    private int _count;

    [SerializeField] private IPuzzleComponent trigger;
    [SerializeField] [CanBeNull] private DialogContent previousDialog;
    private void Awake()
    {
        trigger = GetComponent<IPuzzleComponent>();
        Debug.Log(trigger);
        repCount = 0;
    }
    
    public IPuzzleComponent GetTrigger() => trigger;
    
    public bool CanPlay()
    {
        if (previousDialog != null)
        {
            return previousDialog.trigger.CheckCompletion();
        }
        return true;
    }
    public List<string> GetLines()
    {
        //if been repeated could change...

        return dialog;
    }

    public void HandleRepetition(ref List<DialogContent> list)
    {
        if(!isRepeated) list.Add(this);
        else repCount++;
    }
}
