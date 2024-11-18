using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour, IPuzzleComponent
{
    
    public int numberOfObjects = 2;
    bool state = false;

    [SerializeField]
    private Interactable interactable;

    private List<GameObject> objectsOnPlate = new List<GameObject>();

    private void Awake()
    {
        interactable.enabled = false;
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("PlateObject"))
        {
            objectsOnPlate.Add(collision.gameObject);
            CheckActivation();

        }

    }


    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("PlateObject"))
        {
            objectsOnPlate.Remove(collision.gameObject);
            CheckActivation();

        }

    }

    private void CheckActivation()
    {
        if(objectsOnPlate.Count == numberOfObjects)
        {
            ActivatePlate();
        }
        else
        {
            DeactivatePlate();
        }
    }


    private void ActivatePlate()
    {
       // Debug.Log("Placa de Pressao Ativada.");
       state = true;
        SetDoorState(true);

        EventManager.Instance.OnAiInteraction(this);
    }

    private void DeactivatePlate()
    {
       // Debug.Log("Placa de Pressao Desativada.");
        state = false;
        SetDoorState(false);
    }

    private void SetDoorState(bool isOpen)
    {
        if (interactable != null)
        {
            interactable.enabled = true; 
        }
    }

    public bool CheckCompletion()=> state;

    public void ResetPuzzle()
    {
       state = false;
    }
}
