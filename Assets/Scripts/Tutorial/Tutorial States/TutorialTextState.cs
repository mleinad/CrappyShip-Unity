using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTextState : TutorialBaseState
{
    private string hintText;
    
    [SerializeField]
    GameObject Hint;
    
    [SerializeField]
    private TMP_Text HintText;
    [SerializeField]
    private TMP_Text instructionText;
    
    public override void EnterState(TutorialUIManager context)
    {
    }

    public override void UpdateState(TutorialUIManager context)
    {
        HintText.text = hintText;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            context.SwithState(context.offState);
        }
    }

    public override void ExitState(TutorialUIManager context)
    {
        
    }

    public void SetHintText(string text)
    {
        hintText = text;
    }
}
