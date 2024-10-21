using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalEvaluator : MonoBehaviour, ISignalModifier
{

    int signal;
    
    public int mode =0,  i1, i2, e1, o1; 
    public int count;
    Dictionary<ColliderIO, int> Collider_value_list;
    
    public void Awake(){
        Collider_value_list = new Dictionary<ColliderIO, int>();
    }
    public int GetOutput(ColliderIO collider, int value)
    {

        if(!Collider_value_list.ContainsKey(collider))
        {
            Collider_value_list.Add(collider, value);
        }
        

        if(collider.Get_Type() == InputType.InOut)
        {
            SetSignal(value);
            e1 = value;
            signal *= CalculateSignal();
        }


        return signal;
    }

    public int GetSignal() => signal;

    public void SetSignal(int signal)
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        count = Collider_value_list.Count;
    }

    int CalculateSignal()
    {   
        
        List<int> inputTypeZeroValues = new List<int>();

        foreach (var kvp in Collider_value_list)
        {
            if (kvp.Key.Get_Type() == InputType.input)
            {
                inputTypeZeroValues.Add(kvp.Value);
            }
            
        }
        
         if (inputTypeZeroValues.Count != 2)
        {
            Debug.LogWarning("There are not exactly two colliders with InputType = 0");
            return 0;
        }

        int value1 = inputTypeZeroValues[0];
        int value2 = inputTypeZeroValues[1];

        i1 = value1;
        i2 = value2;

        switch (mode)
        {
            case 0:
            if(value1 == value2) return 1; else return 0;

            case 1:
            if(value1 > value2) return 1; else return 0;

            case 2:
            if (value1>100 && value2 >100) return 1; else return 0;

            default:
            return 0;
        }

    }

    public void Disconnect(ColliderIO current)
    {   
       Collider_value_list[current] = 0;
    }
}
