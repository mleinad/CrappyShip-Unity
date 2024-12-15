using UnityEngine;

public class HintPointState : HintBaseState
{
    public override void EnterState(HintsUIManager context)
    {
        context.arrow.enabled = true;   
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
        
    }

    public override void Hide(HintsUIManager context)
    {
        context.arrow.enabled = false;    
    }
}
