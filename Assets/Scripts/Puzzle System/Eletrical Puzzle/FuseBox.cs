using System.Collections.Generic;
using UnityEngine;


public class FuseBox : MonoBehaviour, IPuzzleComponent, IEletricalComponent
{
   public bool state= false;
    public int signal;
    public int signal_needed;
    
    public Light light;

    Dictionary<IEletricalComponent, ColliderIO> adjencency_dictionary;
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
        adjencency_dictionary = new Dictionary<IEletricalComponent, ColliderIO>();
        adj_comp_names = new List<string>();
    }

    public void ResetPuzzle()=> state = false;

    void Update()
    {
        if (fuse != null)
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

    }

    void ShortFuse()
    {

         fuse.ShortFuse(); 
         fuse = null;
    }
    

    public Dictionary<IEletricalComponent, ColliderIO> GetAdjacencies()=> adjencency_dictionary;
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

    public void SetSignal(int newSignal)
    {
        signal = newSignal;
    }

     public void PropagateSignal()
    {
        if(!fuse) return;
        foreach (var component in adjencency_dictionary.Keys)
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
