using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class SignalBoost : MonoBehaviour, ISignalModifier
{
    [SerializeField]
    int signal, boost;

    bool is_over_base, is_docked;
    
    private float rayDistance = 10.0f;
    DragNDrop dragNDrop;
    ModuleBase base_t;    
    Rigidbody rigidbody;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        dragNDrop = GetComponent<DragNDrop>();
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


    void SwitchInputs()
    {
        
        if(base_t!=null)
        {
            ColliderIO[] col_list = base_t.GetColliders();
            
            ColliderIO strongest_input = null;
            int max =0;

            for(int i=0; i<col_list.Count(); i++)
            {
                col_list[i].SwitchType(InputType.output);
                int input_singal =  base_t.GetSignalByInput(col_list[i]);
                if(input_singal>max)
                {
                    max = input_singal;
                    strongest_input = col_list[i];
                }                
            }
            strongest_input.SwitchType(InputType.input);
            
        }
    }


}
