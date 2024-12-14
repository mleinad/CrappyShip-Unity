using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HintBaseState: MonoBehaviour
{
    public abstract void EnterState(HintsUIManager context);
    
    public abstract void UpdateState(HintsUIManager context);
    
    public abstract void ExitState(HintsUIManager context);
    
    public abstract void Hide();
}
