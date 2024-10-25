using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class Cable : MonoBehaviour, IEletricalComponent
{


    [SerializeField]
     private int signal = 0;


    [SerializeField]
    string t;

    
    List<IEletricalComponent> adjecent_components;
    public List<string> adj_comp_names;


    public int GetSignal()=>signal;

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
        
        signal =0;
  
        SetAdjacenciesSignal();
  
        CheckAdjacencies(); //debug only
//        Debug.DrawLine(transform.position, transform.forward,  Color.magenta);
    }

    void SetAdjacenciesSignal()
    {
         foreach(IEletricalComponent eletricalComponent in adjecent_components)
        {
            if(eletricalComponent.GetSignal()>0)
            {
                signal = eletricalComponent.GetSignal();
            }

        }
    }


}
