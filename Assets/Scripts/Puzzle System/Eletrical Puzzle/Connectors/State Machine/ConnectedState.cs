
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
        //requires checking
        ColliderIO[] colList = context.GetCurrentBase().GetColliders();
            
        ColliderIO strongestInput = null;
        int max =0;

        for(int i=0; i<colList.Count(); i++)
        {
            colList[i].SwitchType(InputType.output);
            int inputSignal =  context.GetCurrentBase().GetSignalByInput(colList[i]);
            if(inputSignal>max)
            {
                max = inputSignal;
                strongestInput = colList[i];
            }                
        }

        if (strongestInput) strongestInput.SwitchType(InputType.input);
    }
    

}
