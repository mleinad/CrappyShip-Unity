using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class SignalRedirect : MonoBehaviour, ISignalModifier
{
    [SerializeField]
    private int signal;
    
    public int GetSignal()=>signal;

    public void SetSignal(int value) => signal = value;


    public void Start()
    {
    }
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

    void OnTriggerExit(Collider other)
    {
        signal = 0;
    }   

}
