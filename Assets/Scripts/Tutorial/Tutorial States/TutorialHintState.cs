using TMPro;
using UnityEngine;

public class TutorialHintState : TutorialBaseState
{
    string ShowHint = "Show Hint";//possibly improve 
    [SerializeField]
    GameObject Hint;
    
    public TMP_Text HintText;
    [SerializeField]
    private TMP_Text instructionText;
    
    public override void EnterState(TutorialUIManager context)
    {
        Hint.SetActive(true);
            HintText.text = ShowHint;
    }

    public override void UpdateState(TutorialUIManager context)
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //instructionText.gameObject.SetActive(false);
            
            //to be determined by puzzle component
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ExitState(context);
        }
    }

    public override void ExitState(TutorialUIManager context)
    {
        Hint.SetActive(false);
    }
}
