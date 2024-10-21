using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRotation : MonoBehaviour
{

    [SerializeField]
    Light light;
    // Start is called before the first frame update
    [SerializeField]
    PuzzleComposite valve_puzzle;
    
    [SerializeField]
    ValvePuzzle valve1, valve2, valve3;

    [SerializeField]
    Transform object_transform;
    
    void Start()
    {
        EventManager.Instance.onTriggerSolved += Solved;    
    }

    // Update is called once per frame
    void Update()
    {
        object_transform.localScale = new Vector3(1,1,1)*MathF.Abs(valve1.GetCurrentAngle()*0.05f); 

    }


    void Solved(PuzzleComposite composite){
       
        if(composite == valve_puzzle)
        {
            light.color = Color.green;
            Debug.Log("solved!");
        }
    }
}
