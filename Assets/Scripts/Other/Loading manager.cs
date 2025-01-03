using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadingmanager : MonoBehaviour
{
    public List<GameObject> roomList;
    public float activationRadius = 50f;
    public float forwardRange = 100f;
    
    public static Loadingmanager Instance;
    private void Awake()
    {
        Instance = this;
        DisableRooms();
        
    }

    private void Update()
    {
        EnableRoomsWithinRadius();
        
        LookForward(Player.Instance.GetPlayerPositon(), Player.Instance.transform.forward, forwardRange);
    }

    void EnableRoomsWithinRadius()
    {
        foreach (GameObject room in roomList)
        {
            float distance = Vector3.Distance(Player.Instance.GetPlayerPositon(), room.transform.position);

            room.transform.parent.gameObject.SetActive(distance <= activationRadius);
        }
    }
    
    void OnDrawGizmos()
    {
        if (!Player.Instance) return;
        Gizmos.color = new Color(0f, 1f, 0f, 0.3f); // Green with some transparency
        Gizmos.DrawSphere(Player.Instance.GetPlayerPositon(), activationRadius);
    }
    void DisableRooms()
    {
        foreach (GameObject room in roomList)
        {
            room.SetActive(false);
        }
    }
    
    
    public void LookForward(Vector3 startPosition, Vector3 direction, float maxDistance)
    {
        if (Physics.Raycast(startPosition, direction, out RaycastHit hit, maxDistance))
        {
            Debug.DrawRay(startPosition, direction * maxDistance, Color.red);            
        }
    }

}
