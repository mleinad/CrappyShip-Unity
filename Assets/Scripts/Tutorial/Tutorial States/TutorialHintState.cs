using System;
using TMPro;
using UnityEngine;

public class TutorialHintState : TutorialBaseState
{
    string ShowHint = "Show Hint";//possibly improve 
    string instructions =  "Enter: show\n" +
                           "Backspace: ignore";
    [SerializeField]
    GameObject Hint;
    
    [SerializeField]
    private TMP_Text HintText;
    [SerializeField]
    private TMP_Text instructionText;
    
    public override void EnterState(TutorialUIManager context)
    {
        Hint.SetActive(true);
        HintText.text = ShowHint;
        
        instructionText.text =instructions;
    }

    public override void UpdateState(TutorialUIManager context)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
               
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            context.SwithState(context.offState);
        }
    }

    public override void ExitState(TutorialUIManager context)
    {
        Hint.SetActive(false);
    }
    
    
    
    //on start
    private void Awake()
    {
        Hint.SetActive(false);
    }
}
