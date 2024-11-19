
public class DroppedState : ConnectorBaseState
{
    public override void EnterState(ConnectorStateManager context)
    {
        context.GetCurrentBase(null);
    }

    public override void UpdateState(ConnectorStateManager context)
    {
        if (context.GetDragNDrop().IsPickedUp())
        {
            context.SwitchState(context.heldState);
        }
    }
    public override void ExitState(ConnectorStateManager context)
    {
        
    }
    
}
