using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalBoost : MonoBehaviour, ISignalModifier
{
    [SerializeField]
    int signal, boost;

    public int GetOutput(string context, int value)
    {
         if(context == "In")
        {
            SetSignal(value);
            return value;
        }
        else if(context == "Out")
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


    void OnTriggerExit(Collider other)
    {
        signal = 0;
    }   
}
