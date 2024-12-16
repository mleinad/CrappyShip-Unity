using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;
using UnityEngine.Scripting;

public class ConsoleManager : MonoBehaviour
{
    private bool previousConsoleState = false;

    void Update()
    {
        bool isConsoleActive = QuantumConsole.Instance.IsActive;

        if (isConsoleActive != previousConsoleState)
        {
            LockMovement(isConsoleActive);
            previousConsoleState = isConsoleActive;
        }
    }

    void LockMovement(bool state)
    {
        var player = Player.Instance;
        if (player)
        {
            player.LockMovement(state);
            player.LockCamera(state);
            player.DeviceEnabledState(!state);
        }
        if (state)
        {
            Cursor.visible = true; 
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    [Preserve]
    [Command]
    void OpenDoor()
    {
        float sphereRadius = 5f; 
        Vector3 spherePosition = Player.Instance.transform.position; 

        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, sphereRadius);

        foreach (Collider collider in hitColliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.enabled = true;

                Debug.Log($"Enabled Interactable on: {collider.gameObject.name}");
            }
        }
    }
}
