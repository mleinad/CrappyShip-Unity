using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    input =0,
    output =1,
    off = 2,
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


    //checks automatically through the name
    if(transform.name.Contains("input")) type = InputType.input;
    if(transform.name.Contains("output")) type = InputType.output;
    if(transform.name.Contains("off")) type = InputType.off;

    transform.name = transform.name + " " + ID;
   }

    public int GetID()=> ID;
    public InputType GetInputType()=> type;
    public IEletricalComponent GetEletricalComponent() => component;

    public Collider GetCollider() => this.GetComponent<Collider>();

    public void SwitchType(InputType new_type)
    {
        type = new_type;
        transform.name = type + " " + ID;
    }

    void OnTriggerEnter(Collider other) //delegates enter trigger logic to module base
    {
        if(component == null) return;

        component.OnChildrenTriggerEnter(this, other);
    }


    void OnTriggerExit(Collider other)
    {
        if(component == null) return;

        component.OnChildrenTriggerExit(this, other);
    }
}