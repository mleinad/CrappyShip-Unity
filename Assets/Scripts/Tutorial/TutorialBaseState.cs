using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialBaseState: MonoBehaviour
{
    public abstract void EnterState(TutorialUIManager context);
    
    public abstract void UpdateState(TutorialUIManager context);
    
    public abstract void ExitState(TutorialUIManager context);
}
