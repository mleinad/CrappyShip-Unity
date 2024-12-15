using UnityEngine;

public class HintTextState : HintBaseState
{
    private string content;
    private string instruction = "press esc to hide hint";
    

    
    public override void EnterState(HintsUIManager context)
    {
        context.hintText.gameObject.SetActive(true);
        context.instructionText.gameObject.SetActive(true);
        context.backpanel.enabled = true;
        context.instructionText.text = instruction;
    }

    public override void UpdateState(HintsUIManager context)
    {
        context.hintText.text = content;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            context.SwitchState(context.offState);
        }
    }

    public override void ExitState(HintsUIManager context)
    {
        Hide(context);
    }

    public void SetHintText(string text)
    {
        content = text;
    }

    public override void Hide(HintsUIManager context)
    {
        context.hintText.gameObject.SetActive(false);
        context.instructionText.gameObject.SetActive(false);
        context.backpanel.enabled = false;
    }
}
