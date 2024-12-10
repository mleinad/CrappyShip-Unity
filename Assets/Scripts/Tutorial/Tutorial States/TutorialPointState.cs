using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPointState : TutorialBaseState
{
    public override void EnterState(TutorialUIManager context)
    {
        
    }

    public override void UpdateState(TutorialUIManager context)
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            context.SwithState(context.offState);
        }
    }

    public override void ExitState(TutorialUIManager context)
    {
        
    }
}
