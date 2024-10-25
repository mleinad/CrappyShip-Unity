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
    List<IEletricalComponent> adjencencies_list;
    Dictionary<ColliderIO, IEletricalComponent> adjencency_dictionary;
    ISignalModifier signalModifier;




    void Awake(){
        
        
        adjencencies_list = new List<IEletricalComponent>();
        adjencency_dictionary = new Dictionary<ColliderIO, IEletricalComponent>();


        colliders = GetComponentsInChildren<ColliderIO>();
    }
    public void Hover()
    {



    }

    void Update()
    {
        CheckAdjacencies();
        DrawVectors();

        if(signalModifier==null) component_name = "no attachements";
        else component_name = signalModifier.ToString();    
    }

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

    public void OnChildrenTriggerEnter(ColliderIO current_collider, Collider other)
    {
        if(current_collider==null) return;

        IEletricalComponent eletricalComponent;
        eletricalComponent = other.GetComponent<IEletricalComponent>();
        
        if(eletricalComponent == null) return;

        if(!adjencencies_list.Contains(eletricalComponent)){
            
            adjencency_dictionary.Add(current_collider, eletricalComponent);
            adjencencies_list.Add(eletricalComponent);
            Debug.Log(current_collider.ToString() + " -> " + eletricalComponent.ToString());
        }
    }

   public void OnChildrenTriggerExit(ColliderIO current_collider, Collider other)
   {        

        if(current_collider==null) return;

        IEletricalComponent eletricalComponent;
        eletricalComponent = other.GetComponent<IEletricalComponent>();

        if(eletricalComponent == null) return;

        if(adjencencies_list.Contains(eletricalComponent)){
            
            adjencencies_list.Remove(eletricalComponent);
            adjencency_dictionary.Remove(current_collider);
        }
   }
    

    void CheckAdjacencies()
    {
        cable_names = new List<string>();

        foreach(KeyValuePair<ColliderIO, IEletricalComponent> key_pair in adjencency_dictionary){
            cable_names.Add("ex");
        } 
    }

    public int GetSignal()
    {
        return 1;
    }
}
