using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    Interactable interactable;
    public Camera camera;

    [SerializeField]
    CanvasGroup canvasGroup;
    void Start()
    {
        EnableTerminal(false);
        interactable = GetComponent<Interactable>();
    }

    void Update()
    {

        if(interactable.WasTriggered())
        {   

            //temporary solution: on trigger Player's crosshair bust be disabled in order to press the Terminal UI elemets
            camera.enabled = true;

            Player.Instance.CrosshairFullOff();
            Player.Instance.LockMovement(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            Player.Instance.DeviceEnabledState(false);

            EnableTerminal(true); 
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EnableTerminal(false);
            camera.enabled = false;
            Player.Instance.LockMovement(false);
                 Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            interactable.SetTrigger(false);
            
            Player.Instance.DeviceEnabledState(true);
            Player.Instance.CrosshairOff();
        }
    }
    
    void EnableTerminal(bool state)
    {
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }



}
