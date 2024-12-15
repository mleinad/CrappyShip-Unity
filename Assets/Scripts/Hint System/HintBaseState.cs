public abstract class HintBaseState
{
    public abstract void EnterState(HintsUIManager context);
    
    public abstract void UpdateState(HintsUIManager context);
    
    public abstract void ExitState(HintsUIManager context);
    
    public abstract void Hide(HintsUIManager context);
}
