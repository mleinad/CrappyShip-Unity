using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



//client
public class PuzzleManager : MonoBehaviour
{

[SerializeField]
PuzzleComposite composite;

    void Start()
    {
      //  EventManager.Instance.onTriggerSolved += solved;
    }
    void Update()
    {
            if(composite.CheckCompletion())
            {
                EventManager.Instance.OnTriggerSolved(composite);
            
                Debug.Log("Move on to next room!");
            }
    }


}
