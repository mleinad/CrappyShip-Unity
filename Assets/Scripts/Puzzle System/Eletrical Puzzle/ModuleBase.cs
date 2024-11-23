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
        cable_names = new List<string>();
        adjencency_dictionary = new Dictionary<IEletricalComponent, ColliderIO>();
        colliders = GetComponentsInChildren<ColliderIO>();
    }

    void Update()
    {
        DrawVectors();
        component_name = signalModifier == null ? "no attachments" : signalModifier.ToString();
        ResetSignal();
    }

    void ResetSignal()
    {
        int count = colliders.Count(col => col.GetInputType() == InputType.input);
        if(count == 0) signal = 0;
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
            cable_names.Add(eletricalComponent.ToString());
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
            cable_names.Remove(eletricalComponent.ToString());
        }
   }
    
#endregion

    public int GetSignalByColliderIO(ColliderIO co)
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
    public int GetSignal()=>signal;
    public void SetSignal(int value)
    {
        signalModifier.SetSignal(adjencency_dictionary);
        signal = signalModifier.GetOutput(); // Update signal after modification
    }
    public Dictionary<IEletricalComponent, ColliderIO> GetAdjacencies()
    {
        return adjencency_dictionary;
    }
    public ColliderIO[] GetColliders()=> colliders;
}
