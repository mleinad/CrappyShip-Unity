using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    List<IPuzzleBehavior> steps;
    private int count = 0;

    // Update is called once per frame
    void Update()
    {

        foreach(var step in steps){
            if(step.CheckCompletion())
            {
                count++;
            }
            if(count == steps.Count) Solved();
        }
        count = 0;
    }


    void Solved()
    {
    
    }
}
