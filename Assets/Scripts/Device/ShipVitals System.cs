using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipVitalsSystem : MonoBehaviour
{

    private float maxOxygen = 100f;
    private float currentOxygen;

    private List<String> tasks= new List<String>();
    private List<String> remainingTasks = new List<String>();


    void Start()
    {
        currentOxygen = maxOxygen;

        tasks.Add("Turn on Lights");
        tasks.Add("Repair Central Computer");
        tasks.Add("Refuel Ship");
        tasks.Add("Turn Navigation Sytem on");

        remainingTasks = new List<String>(tasks);
        
    }

    void Update()
    {
        
    }
}
