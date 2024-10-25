using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    input =0,
    output =1,
    InOut =2,
    error =-1
};

[RequireComponent(typeof(BoxCollider))]

public class ColliderIO : MonoBehaviour
{

    IEletricalComponent component;
    int ID;
   public InputType type;
   
   void Awake()
   {
    component= GetComponentInParent<IEletricalComponent>();
    ID = transform.GetSiblingIndex();


    //checks automatically thru the name
    if(transform.name.Contains("In")) type = InputType.input;
    if(transform.name.Contains("Out")) type = InputType.output;
    if(transform.name.Contains("I/O")) type = InputType.InOut;
   }

    public int GetID()=> ID;
    public InputType GetInputType()=> type;
    public IEletricalComponent GetEletricalComponent() => component;

    public Collider GetCollider() => this.GetComponent<Collider>();


    void OnTriggerEnter(Collider other) //delegates enter trigger logic to module base
    {
        if(component == null) return;

        if(component is ModuleBase moduleBase)
        {
            moduleBase.OnChildrenTriggerEnter(this, other);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if(component == null) return;

        if(component is ModuleBase moduleBase)
        {
            moduleBase.OnChildrenTriggerExit(this, other);
        }
    }
}