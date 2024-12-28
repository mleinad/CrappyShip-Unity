using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class    DragNDrop : MonoBehaviour
{
    private bool interactable, pickedup;
    private Rigidbody objRigidbody;
    public float throwAmount;

    public GameObject currentRoom;
    public Vector3 spawnLocation;
    
    void Awake()
    {
        objRigidbody = GetComponent<Rigidbody>();
        
        spawnLocation = transform.position;
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
                OnPickUp();

            }
            if (Input.GetMouseButtonUp(0))
            {
              OnDrop();

            }
        }
    }

    private void OnDrop()
    {
        objRigidbody.useGravity = true;
        objRigidbody.isKinematic = false;
        pickedup = false;
        
    }

    private void OnPickUp()
    {
        transform.parent = Player.Instance.GetMainCameraTransform(); //gets camera transform from player singleton class
        objRigidbody.useGravity = false;
        objRigidbody.isKinematic = true;
        pickedup = true;
    }

    private void FindCurrentRoom()
    {
        GameObject closestRoom = null;
        float closestDistance = float.MaxValue;

        foreach (var room in Loadingmanager.Instance.roomList)
        {
            float distance = Vector3.Distance(transform.position, room.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestRoom = room;
            }
        }
        
    }
    public bool IsPickedUp()=> pickedup;
}
