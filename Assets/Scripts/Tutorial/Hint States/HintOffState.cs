using UnityEngine;

public class HintOffState : HintBaseState
{
    public override void EnterState(HintsUIManager context)
    {
        Color color = context.backdropImage.color;
        color.a = 0; // Set alpha to 0
        context.backdropImage.color = color;
        
        context.mediaState.Hide(context);
        context.textState.Hide(context);
        context.requestHintState.Hide(context);
        context.pointState.Hide(context);
    }

    public override void UpdateState(HintsUIManager context)
    {
        
    }

    public override void ExitState(HintsUIManager context)
    {
        
    }

    public override void Hide(HintsUIManager context)
    {
        //maybe enable backdrop here
        
    }
}
