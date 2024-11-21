using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IEletricalComponent
{
    [SerializeField]
    private int signal;

    
    List<IEletricalComponent> adjecent_components;
    public List<string> adj_comp_names;
    public List<IEletricalComponent> GetAdjacencies() => adjecent_components;
    
    void Start()
    {    
        adjecent_components = new List<IEletricalComponent>();
    }

    public void OnChildrenTriggerEnter(ColliderIO current_collider, Collider other)
    {
        if(current_collider==null) return;

        IEletricalComponent eletricalComponent;
        eletricalComponent = other.GetComponent<IEletricalComponent>();
        
        if(eletricalComponent == null) return;

        if(!adjecent_components.Contains(eletricalComponent)){
            adjecent_components.Add(eletricalComponent);

        }
    }

    public void OnChildrenTriggerExit(ColliderIO current_collider, Collider other)
    {
        if(current_collider==null) return;

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
        foreach (var component in adjecent_components)
        {
           if(component is not ModuleBase)
           {
             
                    component.SetSignal(signal);  
           }
        }
    }

}
