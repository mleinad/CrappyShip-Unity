using System.Collections.Generic;
using UnityEngine;

public class PuzzleComposite : MonoBehaviour, IPuzzleComponent
{

    [SerializeField]
    List<GameObject> gameObjects;


    private bool been_solved = false;
     
     
    private bool state = false; 
    
    private readonly List<IPuzzleComponent> _components = new List<IPuzzleComponent>();

    public bool State
    {
        get => state;
        set
        {
            if (state != value)
            {
            state = value;
            Debug.Log($"State changed to: {state}");
            if (state)
            {
                Debug.Log("solved!x");
                EventManager.Instance.OnTriggerSolved(this);
                been_solved = true;
            }
            }
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
    
    
    void Awake()
    {
        foreach(GameObject g in gameObjects)
        {
            Add(g.GetComponent<IPuzzleComponent>());
        }
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

    private void Add(IPuzzleComponent component)
    {
        _components.Add(component);
    }

    private void Remove(IPuzzleComponent component)
    {
        _components.Remove(component);
    }
}
