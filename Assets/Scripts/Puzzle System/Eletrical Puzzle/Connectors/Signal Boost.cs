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


    public int GetOutput()
    {
        return signal * boost;
    }

    void GetClosestBase()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down;

        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo, rayDistance))
        {
            
         
            if(base_t) base_t.SetComponent(null);
            
            ModuleBase moduleBase = hitInfo.collider.GetComponent<ModuleBase>();
            if(!moduleBase)
            {
                is_over_base = false;
                return;
            }
            base_t = moduleBase;
            is_over_base = true;
        }

        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
    } 

    void SnapToBase(Transform base_transform)
    {
        transform.position = base_transform.GetChild(0).position;
        is_docked =true;
        base_t.SetComponent(this);
        SwitchInputs();

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
        Debug.Log("snaped to base!");
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
