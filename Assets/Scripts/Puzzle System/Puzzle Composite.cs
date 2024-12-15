using System.Collections.Generic;
using UnityEngine;
using System;

public class PuzzleComposite : MonoBehaviour, IPuzzleComponent
{

    [SerializeField]
    List<GameObject> gameObjects;

    public GameObject solvedPrefab;
    
    
    private bool been_solved = false;
     
     
    private bool state = false; 
    
    private readonly List<IPuzzleComponent> _components = new List<IPuzzleComponent>();

    //handles hints...
    private Dictionary<IPuzzleComponent, Action> hintDictionary;

    public bool State
    {
        get => state;
        set
        {
            if (state != value)
            {
            state = value;
//            Debug.Log($"State changed to: {state}");
            if (state)
            {
  //              Debug.Log("solved!x");
                EventManager.Instance.OnTriggerSolved(this);
                been_solved = true;
            }
            }
        }
    }


    void Awake()
    {
        foreach(GameObject g in gameObjects)
        {
            Add(g.GetComponent<IPuzzleComponent>());
        }
    }

    public bool CheckCompletion()
    {
        if (_components != null)
        {
            foreach (IPuzzleComponent component in _components)
            {
                if (!component.CheckCompletion())
                {
                    return false; // Return immediately if any component is incomplete
                }
            }
        }
        return true; 
    }

    public void ResetPuzzle()
    {
        foreach(IPuzzleComponent component in _components){
            component.ResetPuzzle();
        }
        State = false;
        been_solved = false;
    }
    

    void Update()
    {   if(!been_solved) State = CheckCompletion();
    }


    public void Solve()
    {
        been_solved = true;
        state = true;
        
        Instantiate(solvedPrefab);
    }
    
    private void Add(IPuzzleComponent component)
    {
        _components.Add(component);
    }

    private void Remove(IPuzzleComponent component)
    {
        _components.Remove(component);
    }
}
