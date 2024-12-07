using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//working title
public class RoomLights : MonoBehaviour
{
    [SerializeField]
    private List<Light> lights;

    private void Awake()
    {
        //programmatically load on awake
        //if list empty
        FindLightsRecursive(transform);
    }
    
    public void FindLightsRecursive(Transform parent)
    {
        Light light = parent.GetComponent<Light>();
        if (light != null)
        {
            lights.Add(light);
        }

        foreach (Transform child in parent)
        {
            FindLightsRecursive(child);
        }
    }
    public void TurnOnLights(bool state)
    { 
        foreach(var light in lights)
        {
            light.enabled = state;
        }
    }
}
