using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class DragNDrop : MonoBehaviour
{
    private bool interactable, pickedup;
    private Rigidbody objRigidbody;
    public float throwAmount;


    void Awake()
    {
        objRigidbody = GetComponent<Rigidbody>();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {

            Player.Instance.CrosshairOn();
            interactable = true;
        
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if(pickedup == false)
            {
        
                Player.Instance.CrosshairOff();
                interactable = false;
        
            }
        }
    }
    void Update()
    {
        if (interactable == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                transform.parent = Player.Instance.GetMainCameraTransform(); //gets camera transform from player singleton class
                objRigidbody.useGravity = false;
                pickedup = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                transform.parent = null;
                objRigidbody.useGravity = true;
                pickedup = false;
            }
        }
    }
}
