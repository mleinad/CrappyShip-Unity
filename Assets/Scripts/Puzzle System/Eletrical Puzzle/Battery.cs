using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IEletricalComponent
{
    [SerializeField]
    private int signal;

    
    Dictionary<IEletricalComponent, ColliderIO> adjencency_dictionary;

    public List<string> adj_comp_names;
    public Dictionary<IEletricalComponent, ColliderIO> GetAdjacencies() => adjencency_dictionary;
    
    void Start()
    {    
        adjencency_dictionary = new Dictionary<IEletricalComponent, ColliderIO>();
    }

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

     void CheckAdjacencies()
    {
        adj_comp_names = new List<string>();

        foreach(IEletricalComponent comp in adjencency_dictionary.Keys){
            adj_comp_names.Add(comp.ToString());
        } 
    }



    public int GetSignal() => signal;

    public void SetSignal(int value)
    {
        //PropagateSignal();    //doesnt set signal, always on
    }


    void Update()
    {
        
        PropagateSignal();
        
        CheckAdjacencies();

    }

    public void PropagateSignal()
    {
        foreach (var component in adjencency_dictionary.Keys)
        {
           if(component is not ModuleBase)
           {
             
                    component.SetSignal(signal);  
           }
        }
    }

}
