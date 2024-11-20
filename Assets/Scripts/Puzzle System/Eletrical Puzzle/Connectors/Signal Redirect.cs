using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SignalRedirect : MonoBehaviour, ISignalModifier
{
    [SerializeField]
    private int signal;
    
    private float rayDistance = 10.0f;

    void Update()
    {
        DrawVectors();
    }

    public void SetSignal(int value) => signal = value;



    public int GetOutput()
    {
        return signal;
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
            maxSignal = Mathf.Max(maxSignal, component.Key.GetSignal());
        }
        signal = maxSignal;
    }


    public void HandleInputSwitching(ConnectorStateManager context)
    {
        //requires checking
        ColliderIO[] colList = context.GetCurrentBase().GetColliders();
            
        ColliderIO strongestInput = null;
        ColliderIO output = null;
        int maxSignal =0;
        float maxDot = -Mathf.Infinity;
    
        for(int i=0; i<colList.Count(); i++)
        {
            colList[i].SwitchType(InputType.off);
            int inputSignal =  context.GetCurrentBase().GetSignalByInput(colList[i]);
            
            float dot =  Vector3.Dot(transform.forward, colList[i].transform.forward);

            if (dot > maxDot)
            {
                maxDot = dot;
                output = colList[i];
            }
            
            if(inputSignal>maxSignal)
            {
                maxSignal = inputSignal;
                strongestInput = colList[i];
            }          
            
        }
        
        if (strongestInput) strongestInput.SwitchType(InputType.input);
        if(output) output.SwitchType(InputType.output);


        if (output == strongestInput)//in case is pointing to stronggest input, make input second strongest input
        {
            maxSignal =0;
            strongestInput = null;

            for (int i = 0; i < colList.Count(); i++)
            {
                if (colList[i].GetInputType() == InputType.off)
                {
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
    }
}
