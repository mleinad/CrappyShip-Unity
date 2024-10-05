using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    [SerializeReference]
   List<GameObject> gameObjects;
   
    List<IPuzzleBehavior> steps = new List<IPuzzleBehavior>();
    private int count = 0;


    //TEMPORARY!!!!!!!! VERY BAD 
    void Awake()
    {
        foreach(var g in gameObjects){
            steps.Add(g.GetComponent<IPuzzleBehavior>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(steps!=null){
        foreach(var step in steps){
            if(step.CheckCompletion())
            {
                count++;
            }
            if(count == steps.Count) Solved();
        }
        count = 0;
        }
    }


    void Solved()
    {
        Debug.Log("solved");
    }
}
