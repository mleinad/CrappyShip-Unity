using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintPointState : HintBaseState
{
    [SerializeField] 
    private Image arrow;
    public override void EnterState(HintsUIManager context)
    {
        arrow.enabled = true;   
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

    public override void Hide()
    {
        arrow.enabled = false;    
    }
}
