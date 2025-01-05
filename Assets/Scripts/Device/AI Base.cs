
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AIBase : MonoBehaviour
{
    public GameObject aiPrefab;
    public Transform aiParent;
    
    public PuzzleComposite composite;
    
    bool beenTriggered = false;
    
    private void Start()
    {
        EventManager.Instance.onTriggerSolved += PuzzleSolved;
    }

    void OnTriggerEnter(Collider other)
    {
        
        //compare to check if is player USE singleton
        if (other.transform == Player.Instance.transform)
        {
            if (!beenTriggered)
            { 
                beenTriggered = true;
                InstantiateAI();
            
                
            }
        }
    }
    
    void Display(GameObject target, Transform offset)
    {
        //instantiate target

        Instantiate(target, offset);
        
    }
    
    void PointTowards(GameObject target)
    {
        //instantiate cube around target
        
    }
    private void InstantiateAI()
    {
        Instantiate(aiPrefab, aiParent);
            //further startup logic
        
            
    }

    private void PuzzleSolved(IPuzzleComponent component)
    {
        if (composite == component)
        {
            DestroyAI();
        }
    }
    
    private void DestroyAI()
    {
        Destroy(aiParent);
        
        //further disable logic
    }
}
