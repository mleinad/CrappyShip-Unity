using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestLight : MonoBehaviour, IEletricalComponent
{

    List<IEletricalComponent> adjecent_components;
    public List<string> adj_comp_names;
    public int signal;
     Renderer renderer;
    
    public int GetSignal()
    {
       return signal;
    }


    void Start()
    {    
        adjecent_components = new List<IEletricalComponent>();
        adj_comp_names = new List<string>();
        renderer = GetComponent<Renderer>();
    }


    public void Update()
    {
        if(signal>0)
        {
            renderer.material.color = Color.red;
        }
        else
        {
            renderer.material.color = Color.white;
        }
    
        foreach(var comp in adjecent_components)
        {
            SetSignal(comp.GetSignal());
        }
    }

    void OnTriggerEnter(Collider other)
    {   
        IEletricalComponent electricalComponent;
        electricalComponent = other.GetComponent<IEletricalComponent>();

        if(electricalComponent == null) return;

        if(!adjecent_components.Contains(electricalComponent)){
            adjecent_components.Add(electricalComponent);
            adj_comp_names.Add(electricalComponent.ToString());
        }

    }
    void OnTriggerExit(Collider other)
    {    
        IEletricalComponent eletricalComponent;
        eletricalComponent = other.GetComponent<IEletricalComponent>();

        if(eletricalComponent == null) return;

        if(adjecent_components.Contains(eletricalComponent)){
            adjecent_components.Remove(eletricalComponent);
            adj_comp_names.Remove(eletricalComponent.ToString());
        }
    }
    public void SetSignal(int newSignal)
    {
        signal = newSignal;
    }

    public List<IEletricalComponent> GetAdjacencies()=> adjecent_components;
}
