public abstract class ConnectorBaseState
{
  
    public abstract void EnterState(ConnectorStateManager context);
    
    public abstract void UpdateState(ConnectorStateManager context);
    
    public abstract void ExitState(ConnectorStateManager context);
}
