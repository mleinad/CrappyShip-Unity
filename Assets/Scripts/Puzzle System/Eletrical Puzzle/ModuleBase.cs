using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ModuleBase : MonoBehaviour, IEletricalComponent    
{

    public string component_name;
    public List<string> cable_names;
    
    ColliderIO[] colliders;
    Dictionary<IEletricalComponent, ColliderIO> adjencency_dictionary;

    ISignalModifier signalModifier;

   public int signal;


    void Awake(){
        
        
        adjencency_dictionary = new Dictionary<IEletricalComponent, ColliderIO>();


        colliders = GetComponentsInChildren<ColliderIO>();
    }

    void Update()
    {
        CheckAdjacencies();
        DrawVectors();

        component_name = signalModifier == null ? "no attachments" : signalModifier.ToString();

        SetSignal(0);
    }


#region Snap in place
    void DrawVectors()  //also updates them
    {

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
    public void SetComponent(ISignalModifier modifier){

      signalModifier = modifier;

    }
    public List<Vector3> GetRotationAngles()
    {
    Vector3 front = transform.forward;
    Vector3 back = -transform.forward;
    Vector3 right = transform.right;
    Vector3 left = -transform.right;

    List<Vector3> vectorList = new List<Vector3>()
    {
        front,
        back,
        right,
        left
    };

    return vectorList; 
    }


#endregion
  
#region Trigger logic
    public void OnChildrenTriggerEnter(ColliderIO current_collider, Collider other)
    {
        if(current_collider==null) return;

        IEletricalComponent eletricalComponent;
        eletricalComponent = other.GetComponent<IEletricalComponent>();
        
        if(eletricalComponent == null) return;

        if(!adjencency_dictionary.ContainsKey(eletricalComponent)){
            
            adjencency_dictionary.Add(eletricalComponent, current_collider);
        }
    }

   public void OnChildrenTriggerExit(ColliderIO current_collider, Collider other)
   {        

        if(current_collider==null) return;

        IEletricalComponent eletricalComponent;
        eletricalComponent = other.GetComponent<IEletricalComponent>();

        if(eletricalComponent == null) return;

        if(adjencency_dictionary.ContainsKey(eletricalComponent)){
            
            adjencency_dictionary.Remove(eletricalComponent);
        }
   }
    
#endregion
    
    
    void CheckAdjacencies()
    {
        cable_names = new List<string>();

        foreach(KeyValuePair<IEletricalComponent,ColliderIO> key_pair in adjencency_dictionary)
        {
            cable_names.Add(key_pair.ToString());
        } 
    }

    public InputType GetInputTypeByComponent(IEletricalComponent eletricalComponent)
    {
        if(adjencency_dictionary.ContainsKey(eletricalComponent)) return adjencency_dictionary[eletricalComponent].GetInputType();
        else return InputType.error;
    }
    public int GetSignalByInput(ColliderIO co)
    {
        foreach(var pair in adjencency_dictionary)
        {
            if(pair.Value == co)
            {
                return pair.Key.GetSignal();
            }
        }
        return 0;  
    }

    private ColliderIO GetColliderByComponent(IEletricalComponent eletricalComponent){
        return adjencency_dictionary[eletricalComponent];
    }
    public int GetSignal()=>signal;
    public void SetSignal(int value)
    {
       if (signalModifier == null)
        {
            signal = 0;
        }
        else
        {
            signalModifier.SetSignal(adjencency_dictionary); // Modify signal if modifier present

            signal = signalModifier.GetOutput(); // Update signal after modification
        }

        PropagateSignal();
    }

    public     Dictionary<IEletricalComponent, ColliderIO> GetAdjacencies()
    {
        return adjencency_dictionary;
    }

     private void PropagateSignal()
    {
        foreach (var pair in adjencency_dictionary)
        {
            if (pair.Value.GetInputType() == InputType.output)
            {
                pair.Key.SetSignal(signal);
            
              //  Debug.Log($"Setting signal of {pair.Key} to {signal}");
            }
        }
    }


    public ColliderIO[] GetColliders()=> colliders;
}
