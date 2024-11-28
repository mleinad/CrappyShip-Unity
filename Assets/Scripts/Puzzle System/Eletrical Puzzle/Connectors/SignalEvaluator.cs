using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SignalEvaluator : MonoBehaviour, ISignalModifier
{

    int signal;
    private float rayDistance = 10.0f;
    public int mode =0;
    public int count;
    Dictionary<ColliderIO, int> Collider_value_list;
    public List<int> inputSignals;
    
    public List<TMP_Text> textList;
    private void Start()
    {
        Collider_value_list = new Dictionary<ColliderIO, int>();
    }

    public int GetSignal() => signal;

    // Update is called once per frame
    void Update()
    {
        DrawVectors();
        Display();
        
        
    }

    void Display()
    {
        foreach (var display in textList)
        {
            display.text = GenerateDisplay();
        }
    }

    string GenerateDisplay()
    {
        string outputString = "";
        if (mode == 0)
        {
            outputString += "AND\n\n";
        }

        if (mode == 1)
        {
            outputString += "XOR\n\n";
        }

        foreach (var input in inputSignals)
        {
            outputString += "in: " + input + " ";
        }
        outputString += $"\n out: {output}\n";
        return outputString;
        
    }
    public void Disconnect(ColliderIO current)
    {   
       Collider_value_list[current] = 0;
    }

    public int GetOutput()
    {
        return EvaluateSignal();
    }

    public void SetSignal(Dictionary<IEletricalComponent, ColliderIO> comp)
    {
        
         Collider_value_list.Clear();
         inputSignals.Clear();

         foreach (var pair in comp)
        {
            IEletricalComponent component = pair.Key;
            ColliderIO collider = pair.Value;

            // Check if the component's input type is Input before processing
            if (collider.GetInputType() == InputType.input)
            {
                int componentSignal = component.GetSignal();
                if (componentSignal > 0)
                {
                    Collider_value_list.Add(collider, componentSignal);
                    inputSignals.Add(componentSignal);  //debug            
                }
        
            }
        }

    }

    public void HandleInputSwitching(ConnectorStateManager context)
    {
        ColliderIO[] colList = context.GetCurrentBase().GetColliders();
            
        ColliderIO output = null;
        float maxDot = -Mathf.Infinity;
    
        for(int i=0; i<colList.Count(); i++)
        {
            colList[i].SwitchType(InputType.input);
            int inputSignal =  context.GetCurrentBase().GetSignalByColliderIO(colList[i]);
            
            float dot =  Vector3.Dot(transform.forward, colList[i].transform.forward);

            if (dot > maxDot)
            {
                maxDot = dot;
                output = colList[i];
            }
            
            
        }
        
        if(output) output.SwitchType(InputType.output);
    }

    private int output;
    private int EvaluateSignal()
    {
        output = 0;

        inputSignals = Collider_value_list.Values.ToList();



        if (Collider_value_list.Count() < 2)
        {
            return output;
        }
        
        switch (mode)           //requires reworking... OR gate is altering signal;
        {
            case 0: // AND gate

                if (Collider_value_list.Values.Distinct().Count() == 1 && Collider_value_list.Values.First() != 0)
                {
                    output = Collider_value_list.Values.Max();
                }
                else output = 0;
                break;

            case 1: // XOR gate
               
               if(Collider_value_list.Values.Distinct().Count() == Collider_value_list.Values.Count() && Collider_value_list.Values.First() != 0)
               {
                output = Collider_value_list.Values.Max();    
               }
               break;

            default:
                Debug.Log("Unknown mode for SignalEvaluator");
                break;
        }

        return output;
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
}
