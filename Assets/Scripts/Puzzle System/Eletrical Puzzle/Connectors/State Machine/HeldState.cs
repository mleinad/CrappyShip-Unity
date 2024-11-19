
using JetBrains.Annotations;
using UnityEngine;

public class HeldState : ConnectorBaseState
{
    [CanBeNull] private ModuleBase hoverBase;
    public override void EnterState(ConnectorStateManager context)
    {
        
    }
    public override void UpdateState(ConnectorStateManager context)
    {
        GetClosestBase(context);
        
        if (!context.GetDragNDrop().IsPickedUp())
        {
            if (hoverBase)
            {
                context.SwitchState(context.connectedState);
            }
            else
            {
                context.SwitchState(context.droppedState);
            }
        }
    }
    public override void ExitState(ConnectorStateManager context)
    {
        
    }


    private void GetClosestBase(ConnectorStateManager context)
    {
        Vector3 rayOrigin = context.transform.position;
        Vector3 rayDirection = Vector3.down;
        
        RaycastHit hitInfo;
        
        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo, 10f))
        {
            var moduleBase = hitInfo.collider.gameObject.GetComponent<ModuleBase>();
            if (!moduleBase)
            {
                hoverBase = null;
                return;
            }
            hoverBase = moduleBase;
            context.GetCurrentBase(hoverBase);
        }
        Debug.DrawRay(rayOrigin, rayDirection * 10f, Color.red);
    }
}
