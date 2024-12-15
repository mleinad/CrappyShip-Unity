using UnityEngine;

public class HintPointState : HintBaseState
{
    private Transform highlightObject;
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

        if (highlightObject)
        {
            context.arrow.transform.position = Camera.main.WorldToScreenPoint(highlightObject.position);
        }
    }

    public override void ExitState(HintsUIManager context)
    {
        
    }

    public override void Hide(HintsUIManager context)
    {
        context.arrow.enabled = false;    
    }
    
    public void SetObject(Transform t)
    {
        highlightObject = t;
    }
}
