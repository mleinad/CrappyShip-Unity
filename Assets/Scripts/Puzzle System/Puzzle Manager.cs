using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    [SerializeReference]
    List<GameObject> puzzle_obj;

    [SerializeReference]
    List<GameObject> action_obj;
    
    List<IPuzzleBehavior> steps = new List<IPuzzleBehavior>();
    List<IActions> actions = new List<IActions>();
    

    public bool Ordered;

    private int count = 0;

    private bool BeenSolved = false;


    //TEMPORARY!!!!!!!! VERY BAD 
    void Awake()
    {
        foreach(var g in puzzle_obj){
            steps.Add(g.GetComponent<IPuzzleBehavior>());
        }
        
        foreach(var a in action_obj){

            actions.Add(a.GetComponent<IActions>());
            
        } 

    }

    // Update is called once per frame
    void Update()
    {
        if(steps!=null)
        {
        foreach(var step in steps)  //loops each frame -> possibly change to for()
        {
            if(step.CheckCompletion())
            {
                count++;
            }
            if(!BeenSolved)
            {
                if(count == steps.Count)
                {
                    BeenSolved = true;
                    Solved();
                }
            }
            
        }
        count = 0;
        }
    }


    public void Solved()
    {
        if(actions!=null){
        foreach(var a in actions){

         a.Perform();   

        }
        }
            Debug.Log("performed!");
    }
}
