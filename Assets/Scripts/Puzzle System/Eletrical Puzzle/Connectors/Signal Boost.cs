using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class SignalBoost : MonoBehaviour, ISignalModifier
{
    [SerializeField]
    int signal, boost;
    
    private float rayDistance = 10.0f;

    void Update()
    {
        DrawVectors();
    }
    public int GetOutput()
    {
        return signal * boost;
    }


   

    void DrawVectors(){

        float vectorLength = 2.0f;
        
        Vector3 front = transform.position + transform.forward * vectorLength;
        Vector3 back = transform.position - transform.forward * vectorLength;
        Vector3 right = transform.position + transform.right * vectorLength;
        Vector3 left = transform.position - transform.right * vectorLength;

        Debug.DrawLine(transform.position, front, Color.green);  // Front (green)
        Debug.DrawLine(transform.position, back, Color.red);     // Back (red)
        Debug.DrawLine(transform.position, right, Color.blue);   // Right (blue)
        Debug.DrawLine(transform.position, left, Color.yellow);  // Left (yellow)

    }

    public void SetSignal(Dictionary<IEletricalComponent, ColliderIO> adj_comp)
    {

        int maxSignal = 0;
        foreach (var component in adj_comp)
        {
            if(component.Value.GetInputType() == InputType.input)
                maxSignal = Mathf.Max(maxSignal, component.Key.GetSignal());
        }
        signal = maxSignal;
    }

    public void HandleInputSwitching(ConnectorStateManager context)
    {
        //requires checking
        ColliderIO[] colList = context.GetCurrentBase().GetColliders();
            
        ColliderIO strongestInput = null;
        int maxSignal =0;
    
        for(int i=0; i<colList.Count(); i++)
        {
            colList[i].SwitchType(InputType.output);
            int inputSignal =  context.GetCurrentBase().GetSignalByInput(colList[i]);
            
            if(inputSignal>maxSignal)
            {
                maxSignal = inputSignal;
                strongestInput = colList[i];
            }          
        }
        if (strongestInput) strongestInput.SwitchType(InputType.input);
    }
}
