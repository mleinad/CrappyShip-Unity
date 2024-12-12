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

    [SerializeField] 
    private Image backPanel;    
    [FormerlySerializedAs("HintText")] [SerializeField]
    private TMP_Text hintText;
    [SerializeField]
    private TMP_Text instructionText;
    
    private HintBaseState _nextState;
    
    public override void EnterState(HintsUIManager context)
    {
        backPanel.enabled = true;
        
        hintText.gameObject.SetActive(true);
        instructionText.gameObject.SetActive(true);
        
        hintText.text = _showHint;
        instructionText.text =_instructions;
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
       Hide();
    }

    public void SetHint(HintBaseState nextState)
    {
        _nextState = nextState;
    }
    
    public override void Hide()
    {
        backPanel.enabled = false;
        
        hintText.gameObject.SetActive(false);
        instructionText.gameObject.SetActive(false);
    }
}
