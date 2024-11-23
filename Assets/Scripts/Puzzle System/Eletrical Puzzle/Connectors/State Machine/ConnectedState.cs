
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectedState : ConnectorBaseState
{
    public override void EnterState(ConnectorStateManager context)
    {
        context.GetRigidbody().isKinematic = true;
        SnapPosition(context);
        SnapRotation(context);
        SwitchInputs(context);
        
        context.GetCurrentBase().SetComponent(context.GetSignalModifier());
    }

    public override void UpdateState(ConnectorStateManager context)
    {
        if (context.GetSignalModifier() is SignalRedirect)
        {
            SwitchInputs(context);    
        }
        
        if (context.GetDragNDrop().IsPickedUp())
        {
            context.SwitchState(context.heldState);
        }
    }
    public override void ExitState(ConnectorStateManager context)
    {
        TurnOffInputs(context);
        context.GetCurrentBase().SetComponent(null);
    }

    private void SnapPosition(ConnectorStateManager context)
    {
        context.transform.position = context.GetCurrentBase()
            .transform
            .GetChild(0)
            .position;
        
    }

    private void SnapRotation(ConnectorStateManager context)
    {
        List<Vector3> vectors = context.GetCurrentBase().GetRotationAngles();
        
        Vector3 currentForward = Player.Instance.transform.forward;
        Vector3 closestDirection = vectors[0];
       
        float maxDot = -Mathf.Infinity;

        foreach (Vector3 direction in vectors)
        {
            float dot = Vector3.Dot(currentForward, direction);
            if (dot > maxDot)
            {
                maxDot = dot;
                closestDirection = direction;
            }
        }
        context.transform.rotation = Quaternion.LookRotation(closestDirection);
    }

    private void SwitchInputs(ConnectorStateManager context)
    {
       context.GetSignalModifier().HandleInputSwitching(context);
    }

    private void TurnOffInputs(ConnectorStateManager context)
    {
        ColliderIO[] colList = context.GetCurrentBase().GetColliders();
            
        for(int i=0; i<colList.Count(); i++)
        {
            colList[i].SwitchType(InputType.off);
        }
    }
    

}
