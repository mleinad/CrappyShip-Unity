using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RequestHintState : HintBaseState
{
    string _showHint = "Show Hint";//possibly improve 
    string _instructions =  "E: show\n" +
                           "Backspace: ignore";
    
    private HintBaseState _nextState;
    
    public override void EnterState(HintsUIManager context)
    {
        context.backpanel.enabled = true;
        context.hintText.gameObject.SetActive(true);
        context.instructionText.gameObject.SetActive(true);
        context.hintText.text = _showHint;
        context.instructionText.text =_instructions;
    }

    public override void UpdateState(HintsUIManager context)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
               context.SwitchState(_nextState);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            context.SwitchState(context.offState);
        }
    }

    public override void ExitState(HintsUIManager context)
    {
       Hide(context);
    }

    public void SetHint(HintBaseState nextState)
    {
        _nextState = nextState;
    }
    
    public override void Hide(HintsUIManager context)
    {
        context.backpanel.enabled = false;
        context.hintText.gameObject.SetActive(false);
        context.instructionText.gameObject.SetActive(false);
    }
}
