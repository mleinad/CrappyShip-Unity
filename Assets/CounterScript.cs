using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ValveData
{
    public ValvePuzzle valveController; // Reference to the ValveController script
    public float angleThreshold = 500.0f;   // Angle threshold for this specific valve
}

public class CounterScript : MonoBehaviour
{
    public List<ValveData> valves;
    public List<GameObject> numberObjects;

    public Vector3 centerPosition = Vector3.zero;

    private int currentNumber = 0;
    private int previousNumber = -1;

    private Dictionary<ValvePuzzle, int> previousNumbers = new Dictionary<ValvePuzzle, int>();

    private void Start()
    {
        foreach (var valveData in valves)
        {
            previousNumbers[valveData.valveController] = -1;
        }
    }
    private void Update()
    {
        foreach (var valveData in valves)
        {
            // Ensure the valveController is valid
            if (valveData.valveController != null)
            {
                // Get the current angle from the ValveController
                float valveAngle = valveData.valveController.GetCurrentAngle();

                // Calculate the displayed number based on the angle and specific threshold
                int calculatedNumber = Mathf.Clamp((int)(valveAngle / valveData.angleThreshold), 0, numberObjects.Count - 1);

                // Only update if the number has changed for this specific valve
                if (calculatedNumber != previousNumbers[valveData.valveController])
                {
                    ShowNumber(calculatedNumber);
                    previousNumbers[valveData.valveController] = calculatedNumber;
                }
            }
        }
    }
    public void ShowNumber(int number)
    {
        // Validate the number is within range
        if (number < 0 || number >= numberObjects.Count) return;

        // Hide all numbers first
        foreach (var numberObj in numberObjects)
        {
            numberObj.SetActive(false);
        }

        // Show the desired number and move it to the center
        numberObjects[number].SetActive(true);
        numberObjects[number].transform.localPosition = centerPosition;
    }
    

}
