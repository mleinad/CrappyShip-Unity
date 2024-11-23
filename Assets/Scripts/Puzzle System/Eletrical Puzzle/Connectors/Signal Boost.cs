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
    
    public List<int> inputSignals = new List<int>();
    public int GetOutput()
    {
        return signal * boost;
    }

    public void SetSignal(Dictionary<IEletricalComponent, ColliderIO> adjComp)
    { 
        inputSignals.Clear();
        int maxSignal = 0;
        foreach (var component in adjComp)
        {
            if (component.Value.GetInputType() == InputType.input)
            {
                maxSignal = Mathf.Max(maxSignal, component.Key.GetSignal());
            }
            inputSignals.Add(component.Key.GetSignal());
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
            int inputSignal =  context.GetCurrentBase().GetSignalByColliderIO(colList[i]);
            
            if(inputSignal>maxSignal)
            {
                maxSignal = inputSignal;
                strongestInput = colList[i];
            }          
        }
        if (strongestInput) strongestInput.SwitchType(InputType.input);
    }
}
