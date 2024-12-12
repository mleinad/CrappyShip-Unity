using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintOffState : HintBaseState
{
    public override void EnterState(HintsUIManager context)
    {
        Color color = context.backdropImage.color;
        color.a = 0; // Set alpha to 0
        context.backdropImage.color = color;
        
        context.mediaState.Hide();
        context.textState.Hide();
        context.requestHintState.Hide();
        context.pointState.Hide();
    }

    public override void UpdateState(HintsUIManager context)
    {
        
    }

    public override void ExitState(HintsUIManager context)
    {
        
    }

    public override void Hide()
    {
    }
}
