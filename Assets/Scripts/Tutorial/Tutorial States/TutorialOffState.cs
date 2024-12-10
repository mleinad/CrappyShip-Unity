using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOffState : TutorialBaseState
{
    public override void EnterState(TutorialUIManager context)
    {
        Color color = context.backdropImage.color;
        color.a = 0; // Set alpha to 0
        context.backdropImage.color = color;
    }

    public override void UpdateState(TutorialUIManager context)
    {
        
    }

    public override void ExitState(TutorialUIManager context)
    {
        
    }
}
