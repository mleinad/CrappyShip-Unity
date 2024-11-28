using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacted : MonoBehaviour, IPuzzleComponent
{
    bool state;

    [SerializeField]
    Interactable  interactable;


    void Update()
    {
        if(interactable.WasTriggered()&&!state)
        {
            state = true;
            EventManager.Instance.OnAiTrigger(this);
        }
    }

    public bool CheckCompletion()
    {
        return state;
    }

    public void ResetPuzzle()
    {
        state = false;
    }
}
