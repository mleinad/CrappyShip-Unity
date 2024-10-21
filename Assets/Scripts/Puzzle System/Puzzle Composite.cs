using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleComposite : MonoBehaviour, IPuzzleComponent
{

    [SerializeField]
    List<GameObject> gameObjects;


    private bool been_solved = false;
     
     
    public bool state = false; 
    
    private readonly List<IPuzzleComponent> _components = new List<IPuzzleComponent>();



    void Awake()
    {
        foreach(GameObject g in gameObjects)
        {
            Add(g.GetComponent<IPuzzleComponent>());
        }
    }

    public bool CheckCompletion()
    {

        if(_components!= null)
        {
        foreach(IPuzzleComponent component in _components){
            
            if(!component.CheckCompletion()){
                state= false;
                break;
            }else state = true;
        }
        }
        return state;
    }

    public void ResetPuzzle()
    {
        foreach(IPuzzleComponent component in _components){
            component.ResetPuzzle();
        }
        state = false;
    }

    void Update()
    {
        if(state)
        {
        if(!been_solved){
            been_solved = true;
            Debug.Log("soved but no event triggerd");
            EventManager.Instance.OnTriggerSolved(this);
        }
        }
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
