using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ValveData
{
    public ValvePuzzle valveController;       
    public Counter primaryCounter;            
    public Counter secondaryCounter;         
    public float angleThreshold = 100.0f;     
    public float secondaryThreshold = 200.0f; 
}

public class NumberCounter : MonoBehaviour
{
    public List<ValveData> valves;           
    private Dictionary<ValvePuzzle, int> previousPrimarySteps = new Dictionary<ValvePuzzle, int>();
    private Dictionary<ValvePuzzle, int> previousSecondarySteps = new Dictionary<ValvePuzzle, int>();

    private void Start()
    {
        foreach (var valveData in valves)
        {
            previousPrimarySteps[valveData.valveController] = -1;
            previousSecondarySteps[valveData.valveController] = -1;
        }
    }

    private void Update()
    {
        foreach (var valveData in valves)
        {
            if (valveData.valveController != null)
            {
                float valveAngle = Mathf.Abs(valveData.valveController.GetCurrentAngle());
 
                if (valveData.primaryCounter != null)
                {
                    int primarySteps = Mathf.FloorToInt(valveAngle / valveData.angleThreshold);
                    if (primarySteps != previousPrimarySteps[valveData.valveController])
                    {
                        valveData.primaryCounter.SetToDigitAtSteps(primarySteps);
                        previousPrimarySteps[valveData.valveController] = primarySteps;
                    }
                }

                if (valveData.secondaryCounter != null)
                {
                    int secondarySteps = Mathf.FloorToInt(valveAngle / valveData.secondaryThreshold);
                    if (secondarySteps != previousSecondarySteps[valveData.valveController])
                    {
                        valveData.secondaryCounter.SetToDigitAtSteps(secondarySteps);
                        previousSecondarySteps[valveData.valveController] = secondarySteps;
                    }
                }
            }
        }
    }
}