using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterManager : MonoBehaviour
{
    public List<Counter> counters;   
    private bool allCountersReached = false; 

    private void Update()
    {
        
        if (!allCountersReached && AreAllCountersReached())
        {
            Debug.Log("done");
            allCountersReached = true; 
        }
    }

    private bool AreAllCountersReached()
    {
        foreach (var counter in counters)
        {
            if (!counter.isTargetReached)
            {
                return false;
            }
        }
        return true; 
    }
}
