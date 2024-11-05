using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour, IPuzzleComponent
{
    
    private int numberOfObjects = 8;

    [SerializeField]
    private Animator doorAnimator;

    private List<GameObject> objectsOnPlate = new List<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("PlateObject"))
        {
            objectsOnPlate.Add(collision.gameObject);
            CheckActivation();

        }

    }


    private void OnCollisionExit(Collision collision)
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
        Debug.Log("Placa de Pressao Ativada.");
        SetDoorState(true);
    }

    private void DeactivatePlate()
    {
        Debug.Log("Placa de Pressao Desativada.");
        SetDoorState(false);
    }

    private void SetDoorState(bool isOpen)
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("isOpen", isOpen); 
        }
    }

    public bool CheckCompletion()
    {
        throw new System.NotImplementedException();
    }

    public void ResetPuzzle()
    {
        throw new System.NotImplementedException();
    }
}
