using System.Collections.Generic;
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

    void Start()
    {    
        adjecent_components = new List<IEletricalComponent>();
    }

    
    void CheckAdjacencies()
    {
        
        //debug only
        adj_comp_names = new List<string>();

        foreach(IEletricalComponent comp in adjecent_components){
            adj_comp_names.Add(comp.ToString());
        } 
    }

    void Update()
    {


        CheckAdjacencies(); //debug only
    }

    public void PropagateSignal()
    {
        foreach (var component in adjecent_components)
        {
           if(component is not ModuleBase)
           {
                if(component is FuseBox) component.SetSignal(signal);
                
                if (component.GetSignal() < signal)
                {
                    component.SetSignal(signal);
                }


           }
        }
    }

}
