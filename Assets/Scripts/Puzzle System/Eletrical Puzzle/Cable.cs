using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour, IEletricalComponent
{


    [SerializeField]
     private int signal = 0;


    [SerializeField]
    string t;

    public List<string> adj_comp_names;
    Dictionary<IEletricalComponent, ColliderIO> adjencency_dictionary;

    [SerializeField] private bool isPrinting=false;
    
    public int GetSignal()=>signal;

    public void SetSignal(int newSignal)
    {
        signal = newSignal; 
    }

    void Start()
    {    
        adjencency_dictionary = new Dictionary<IEletricalComponent, ColliderIO>();
    }


    public Dictionary<IEletricalComponent, ColliderIO>GetAdjacencies() => adjencency_dictionary;
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


    
    void CheckAdjacencies()
    {
        //debug only
        adj_comp_names = new List<string>();

        foreach(IEletricalComponent comp in adjencency_dictionary.Keys){
            adj_comp_names.Add(comp.ToString());
        } 
    }

    void Update()
    {
        SwitchInputType();
        PropagateSignal();
        CheckAdjacencies(); //debug only
    }

    public void PropagateSignal()
    {
        int inputCount = 0;
        foreach (var adjecency in adjencency_dictionary)
        {
            if (adjecency.Value.GetInputType() == InputType.input)
            {
                int tempSignal = adjecency.Key.GetSignal();
                
                signal = inputCount == 0 ? tempSignal : Mathf.Max(signal, tempSignal);
                inputCount++;
            }
            else if (adjecency.Value.GetInputType() == InputType.output)
            {
                adjecency.Key.SetSignal(signal);
            
                if (isPrinting)
                {
                    Debug.Log("base: " + adjecency.Key + " set to " + signal.ToString());
                }
            }
        } 
        if(inputCount == 0) signal = 0;
    }


    private void SwitchInputType()
    {
        
        foreach (var adjecency in adjencency_dictionary)
        {
            var componentAdjList = adjecency.Key.GetAdjacencies();
            if(componentAdjList.ContainsKey(this))
            {
                var adjCollider = componentAdjList[this];
                switch (adjCollider.GetInputType())
                {
                    case InputType.input:
                        adjecency.Value.SwitchType(InputType.output);
                        break;
                    case InputType.output:
                        adjecency.Value.SwitchType(InputType.input);
                        break;
                    case InputType.off:
                        adjecency.Value.SwitchType(InputType.off);
                        break;
                }
            }
        }
    }

}
