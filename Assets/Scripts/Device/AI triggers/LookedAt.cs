using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookedAt : MonoBehaviour, IPuzzleComponent
{
    bool state;
    public bool CheckCompletion()=> state;

    public void ResetPuzzle()
    {
        state = false;
    }
    void Start()
    {
        state = false;        
    }

    void OnTriggerEnter(Collider other)
    {   
        if(other==null) return;
        if(other.CompareTag("MainCamera"))
        {
            state = true;
            EventManager.Instance.OnAiTrigger(this);
//            Debug.Log("looked at door");
        }
    }
    
    void OnTriggerExit(Collider other)
    {
         if(other==null) return;
        if(other.CompareTag("MainCamera"))
        {
            //ResetPuzzle();
        }
    }
}
