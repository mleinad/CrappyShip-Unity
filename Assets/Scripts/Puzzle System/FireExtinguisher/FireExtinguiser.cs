using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FireExtinguiser : MonoBehaviour
{
    DragNDrop dragNDrop;

    private void Awake()
    {
        dragNDrop = GetComponent<DragNDrop>();
    }

    private void Update()
    {
        if (dragNDrop.IsPickedUp())
        {
            HoldGun();
        }
        
        
    }


    void HoldGun()
    {
        transform.localPosition = new Vector3(0.706f, -0.301f, 1.049f);
        transform.rotation = Player.Instance.GetMainCameraTransform().rotation;
    }
}
