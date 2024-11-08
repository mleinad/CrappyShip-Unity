using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TypingPuzzle : MonoBehaviour, IPuzzleComponent
{
    Interactable interactable;
   public Camera camera;
    public Text word_output;

    [SerializeField]
    CanvasGroup canvasGroup;
    void Start()
    {
        EnableTerminal(false);
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable.WasTriggered())
        {   

            //temporary solution: on trigger Player's crosshair bust be disabled in order to press the Terminal UI elemets
            camera.enabled = true;
            Player.Instance.LockMovement(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

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
        }
    }


    void EnableTerminal(bool state)
    {
         canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
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
