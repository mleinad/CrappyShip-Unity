using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAction : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeReference]
    PuzzleComposite composite;

    float rotation_speed =30f;
    void Start()
    {
        EventManager.Instance.onTriggerSolved += Solved;
    }

    // Update is called once per frame
    void Solved (PuzzleComposite puzzleComposite){
        if(puzzleComposite ==composite){
            
            transform.GetChild(0).Rotate(0, 0, rotation_speed * Time.deltaTime); 
        }
    }
}
