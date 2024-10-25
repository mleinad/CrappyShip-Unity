using System;
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
        if (input.GetInputType() == InputType.input)
        {
            SetSignal(value);
            return value;
        }
        else if(input.GetInputType()==InputType.output)
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
