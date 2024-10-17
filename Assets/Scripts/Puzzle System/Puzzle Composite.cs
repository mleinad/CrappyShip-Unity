using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleComposite : MonoBehaviour, IPuzzleComponent
{

    [SerializeField]
    List<GameObject> gameObjects;

    private bool state = false;
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


    private void Add(IPuzzleComponent component)
    {
        _components.Add(component);
    }

    private void Remove(IPuzzleComponent component)
    {
        _components.Remove(component);
    }
}
