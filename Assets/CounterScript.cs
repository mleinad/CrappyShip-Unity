using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterScript : MonoBehaviour
{
    public List<GameObject> numberObjects;

    public Vector3 centerPosition = Vector3.zero;

    private int currentNumber = 0;
    private int previousNumber = -1;
    public ValvePuzzle valvePuzzle;

    private void Start()
    {
        // Ensure only the starting number is shown in the center
        ShowNumber(currentNumber);
    }
    private void Update()
    {
        // Check if the valveController is assigned
        if (valvePuzzle != null)
        {
            // Get the current angle from the ValveController
            float valveAngle = valvePuzzle.GetCurrentAngle();

            // Update the number based on the angle
            UpdateNumberBasedOnAngle(valveAngle);
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
    public void UpdateNumberBasedOnAngle(float valveAngle)
    {
        // Calculate the number based on the angle (500 degrees per number)
        int calculatedNumber = Mathf.Clamp((int)(valveAngle / 60), 0, numberObjects.Count - 1);

        // Only update if the number has changed
        if (calculatedNumber != previousNumber)
        {
            currentNumber = calculatedNumber;
            ShowNumber(currentNumber);
            previousNumber = currentNumber;
        }
    }

}
