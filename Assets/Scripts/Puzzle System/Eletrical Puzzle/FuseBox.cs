using System.Collections.Generic;
using UnityEngine;


public class FuseBox : MonoBehaviour, IPuzzleComponent, IEletricalComponent
{
   public bool state= false;
    public int signal;
    public int signal_needed;
    
    public Light light;

    List<IEletricalComponent> adjecent_components;
    public List<string> adj_comp_names;
    public Fuse fuse;
    public bool CheckCompletion()=>state;

    public int GetSignal()
    {
       return signal;
    }


     void Start()
    {    
        fuse = GetComponentInChildren<Fuse>();
        adjecent_components = new List<IEletricalComponent>();
        adj_comp_names = new List<string>();
    }

    public void ResetPuzzle()=> state = false;

    void Update()
    {
        if(signal<signal_needed)
        {
            if(signal >0) state = true; 
            else ResetPuzzle();         

            if(fuse) fuse.transform.Rotate(0, 0, signal * Time.deltaTime); 

        }
        else
        {
            if(fuse)
            {
                ShortFuse();
            }
        }
        PropagateSignal();

    }

    void ShortFuse()
    {

         fuse.ShortFuse(); 
         fuse = null;
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
        Debug.Log(other.gameObject.name);

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

    public List<IEletricalComponent> GetAdjacencies()=> adjecent_components;

    public void SetSignal(int newSignal)
    {
        signal = newSignal;
    }

     public void PropagateSignal()
    {
        if(!fuse) return;
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
