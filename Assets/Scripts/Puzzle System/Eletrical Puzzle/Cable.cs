using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Formats.Fbx.Exporter;
using UnityEngine;

public class Cable : MonoBehaviour, IEletricalComponent
{


    [SerializeField]
     private int signal = 0;


    [SerializeField]
    string t;

    
    List<IEletricalComponent> adjecent_components;
    public List<string> adj_comp_names;


    public int GetSignal()=>signal;

    public void SetSignal(int newSignal)
    {
        signal = newSignal;
        PropagateSignal();
    }



    public List<IEletricalComponent> GetAdjacencies() => adjecent_components;
    
    void Start()
    {    
        adjecent_components = new List<IEletricalComponent>();
    }
    void OnTriggerEnter(Collider other)
    {   
        IEletricalComponent electricalComponent;
        electricalComponent = other.GetComponent<IEletricalComponent>();

        if(electricalComponent == null) return;

        if(!adjecent_components.Contains(electricalComponent)){
            adjecent_components.Add(electricalComponent);
        }

    }
    void OnTriggerExit(Collider other)
    {    
        IEletricalComponent eletricalComponent;
        eletricalComponent = other.GetComponent<IEletricalComponent>();

        if(eletricalComponent == null) return;

        if(adjecent_components.Contains(eletricalComponent)){
            adjecent_components.Remove(eletricalComponent);
        }
    }
    void CheckAdjacencies()
    {
        adj_comp_names = new List<string>();

        foreach(IEletricalComponent comp in adjecent_components){
            adj_comp_names.Add(comp.ToString());
        } 
    }

    void Update()
    {
        /*
        int maxSignal = 0;                                   // suspected problem: module base sets the signal at the same time, randomly overlaps 
        foreach (var component in adjecent_components)
        {
              if(component is not ModuleBase)
            {
                maxSignal = Mathf.Max(maxSignal, component.GetSignal());
            }
        }
        SetSignal(maxSignal);
  
    */

        CheckAdjacencies(); //debug only
        //Debug.DrawLine(transform.position, transform.forward,  Color.magenta);
    }

    public void PropagateSignal()
    {
        foreach (var component in adjecent_components)
        {
           if(component is not ModuleBase)
           {
                if (component.GetSignal() < signal)
                {
                    component.SetSignal(signal);
                }   
           }
        }
    }

}
