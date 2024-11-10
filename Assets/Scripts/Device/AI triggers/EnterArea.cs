using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnterArea : MonoBehaviour, IPuzzleComponent
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
        if(other.transform == Player.Instance.transform)
        {
            state = true;
            IPuzzleComponent comp = this.GetComponent<IPuzzleComponent>();
            EventManager.Instance.OnAiInteraction(this);
        }
    }
}
