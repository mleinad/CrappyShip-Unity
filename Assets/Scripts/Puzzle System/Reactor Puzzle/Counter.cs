using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public List<GameObject> numberObjects;   // List of GameObjects for each digit
    public Vector3 centerPosition = Vector3.zero; // Position to center the displayed digit

    public GameObject targetObject;          // The GameObject for the correct number

    public bool isTargetReached { get; private set; }

    private int currentIndex = -1;           // Tracks the currently displayed number's index


    public void Start()
    {
        isTargetReached = false;
    }
    // Method to set the active digit based on a step value
    public void SetToDigitAtSteps(int steps)
    {
        // Calculate the index of the digit to display
        int newIndex = Mathf.Clamp(steps % numberObjects.Count, 0, numberObjects.Count - 1);

        // Only update if the displayed index has changed
        if (newIndex != currentIndex)
        {
            ShowNumber(newIndex);
            currentIndex = newIndex;

            CheckTargetReached();
        }
    }

    // Display the selected number and hide others
    private void ShowNumber(int index)
    {
        // Validate the index is within range
        if (index < 0 || index >= numberObjects.Count) return;

        // Hide all numbers
        foreach (var numberObj in numberObjects)
        {
            numberObj.SetActive(false);
        }

        // Activate and center the target number
        numberObjects[index].SetActive(true);
        numberObjects[index].transform.localPosition = centerPosition;
    }
    private void CheckTargetReached()
    {
        // Check if the current index is equal to the target number's index
        if (numberObjects[currentIndex] == targetObject)
        {
            isTargetReached = true;
            targetObject.SetActive(true); // Activate the target object if reached
            Debug.Log("isTargetReached changed to: " + isTargetReached);
        }
        else
        {
            isTargetReached = false;
            targetObject.SetActive(false); // Ensure the target object is inactive otherwise
        }
    }

}
