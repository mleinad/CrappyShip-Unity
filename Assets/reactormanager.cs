using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reactormanager : MonoBehaviour
{
    [SerializeField] reactorinterpreter reactorinterpreter1;
    [SerializeField] reactorinterpreter reactorinterpreter2;
    public Animator reactor;

    private void Update()
    {
        CheckTerminals();
    }

    void CheckTerminals()
    {
        if(reactorinterpreter1 != null && reactorinterpreter2 != null)
        {
            bool allDone = reactorinterpreter1.CheckCompletion() && reactorinterpreter2.CheckCompletion();
            if (reactor != null)
            {
                reactor.SetBool("reactordone", allDone);
            }
        }
    }
}
