using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalBoost : MonoBehaviour, ISignalModifier
{
    [SerializeField]
    int signal, boost;

    List<IEletricalComponent> inputs;
    public int GetOutput(ColliderIO input, int value)
    {

        if (input.Get_Type() == InputType.input)
        {
            SetSignal(value);
            return value;
        }
        else if(input.Get_Type()==InputType.output)
        {
            return signal;
        }
        else throw new ArgumentException("invaled input type");
    }

    public int GetSignal()
    {
        throw new System.NotImplementedException();
    }

    public void SetSignal(int level) => signal = level * boost;

    void OnTriggerEnter(Collider other)
    {
        ColliderIO collider;
        IEletricalComponent electricalComponent;
        
        collider = other.GetComponent<ColliderIO>();

        if(collider!= null)
        {
            electricalComponent = collider.GetEletricalComponent();

        }else return;

        if (electricalComponent == null)
            return;
    
        if(!inputs.Contains(electricalComponent)){
            inputs.Add(electricalComponent);
        }
    }


    void OnTriggerExit()
    {


    }
    void OnTriggerExit(Collider other)
    {
        signal = 0;
    }   
}
