using UnityEngine;

public class HintMediaState : HintBaseState
{
    public override void EnterState(HintsUIManager context)
    {
        context.mediaPanel.SetActive(true);
    }

    public override void UpdateState(HintsUIManager context)
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            context.SwitchState(context.offState);
        }
    }

    public override void ExitState(HintsUIManager context)
    {
        Hide(context);
    }

    public override void Hide(HintsUIManager context)
    {
        context.mediaPanel.SetActive(false);
    }
}
