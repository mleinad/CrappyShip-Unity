using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    
    private int numberOfObjects = 3;

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
    }

    private void DeactivatePlate()
    {
        Debug.Log("Placa de Pressao Desativada.");
    }


    
}
