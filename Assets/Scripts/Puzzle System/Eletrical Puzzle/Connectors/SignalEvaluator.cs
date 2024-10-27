using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalEvaluator : MonoBehaviour, ISignalModifier
{

    int signal;
    
    
    bool is_over_base, is_docked;
    
    private float rayDistance = 10.0f;
    DragNDrop dragNDrop;
    ModuleBase base_t;  
    public int mode =0;
    public int count;
    Dictionary<ColliderIO, int> Collider_value_list;
    public List<int> inputSignals;
    
    Rigidbody rigidbody;

    public void Awake(){
        rigidbody = GetComponent<Rigidbody>();

        dragNDrop = GetComponent<DragNDrop>();
    }


    public int GetSignal() => signal;

    // Update is called once per frame
    void Update()
    {
        
        if(is_docked) rigidbody.isKinematic =true;

        if(dragNDrop.IsPickedUp())
        {
        is_docked =false;
        GetClosestBase();
        }

        DrawVectors();

        if(is_over_base && !dragNDrop.IsPickedUp())
        {

            if(!is_docked)
            {
                SnapToBase(base_t.transform);
                SnapRotation();
            }
        }

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
        
         Collider_value_list = new Dictionary<ColliderIO, int>();

         foreach (var pair in comp)
        {
            IEletricalComponent component = pair.Key;
            ColliderIO collider = pair.Value;

            // Check if the component's input type is Input before processing
            if (collider.GetInputType() == InputType.input)
            {
                int componentSignal = component.GetSignal();
                
                // Store or update the signal for each input-type collider
                if (Collider_value_list.ContainsKey(collider))
                {
                    Collider_value_list[collider] = componentSignal;
                }
                else
                {
                    Collider_value_list.Add(collider, componentSignal);
                }
            }
        }

    }


     private int EvaluateSignal()
    {
        int output = 0;

        // Collect all input signals into a list
        inputSignals = new List<int>(Collider_value_list.Values);

        if (inputSignals.Count == 0)
            return 0;  // No inputs, return 0

        switch (mode)           //requires reworking... or gate is altering signal;
        {
            case 0: // AND gate
                output = inputSignals[0];  // Start with 1 for AND logic   

                foreach (int sig in inputSignals)
                {
                    output &= sig;
                }
                break;

            case 1: // OR gate
                foreach (int sig in inputSignals)
                {
                    output |= sig;
                }
                break;

            case 2: // XOR gate
                foreach (int sig in inputSignals)
                {
                    output ^= sig;
                }
                break;

            case 3: // EQUAL (all inputs must be the same to output 1)
                bool allEqual = inputSignals.TrueForAll(x => x == inputSignals[0]);
                output = allEqual ? 1 : 0;
                break;

            default:
                Debug.LogWarning("Unknown mode for SignalEvaluator");
                break;
        }

        return output;
    }


    #region Snap in place
  


    void GetClosestBase()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down;

        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo, rayDistance))
        {
            
         
            if(base_t!=null) base_t.SetComponent(null);
            
            ModuleBase moduleBase = hitInfo.collider.GetComponent<ModuleBase>();
            if(moduleBase==null)
            {
                is_over_base = false;
                return;
            }
            base_t = moduleBase;
            is_over_base = true;
            base_t.SetComponent(this);

        }

        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
    } 

    void SnapToBase(Transform base_transform)
    {
        transform.position = base_transform.GetChild(0).position;
        is_docked =true;
    }
    
    void SnapRotation()
    {

        List<Vector3> vectors = base_t.GetRotationAngles();
    
        Vector3 currentForward = Player.Instance.transform.forward; //ALTERNATIVE: transform.forward / relative to this object

        Vector3 closestDirection = vectors[0];
        float maxDot = -Mathf.Infinity;

        foreach (Vector3 direction in vectors)
        {
            float dot = Vector3.Dot(currentForward, direction);
            if (dot > maxDot)
            {
                maxDot = dot;
                closestDirection = direction;
            }
        }

        transform.rotation = Quaternion.LookRotation(closestDirection);
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

    #endregion
}
