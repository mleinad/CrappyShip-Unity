using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

//leaf
public class ButtonPuzzle : MonoBehaviour, IPuzzleComponent 
{


    [SerializeField]
    bool press_and_hold;
    public bool interactable, pickedup;
    private bool state = false;
    // Start is called before the first frame update
   public void StartPuzzle()
   {

   }

    public bool CheckCompletion()
    {
        return state;
    }

    public void ResetPuzzle()
    {
        state = false;
    }  

    void Update(){


        if(Input.GetMouseButtonDown(0)){
                if(interactable) state = true;
        }

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
                Player.Instance.CrosshairOff();
                interactable = false;
        }
    }

}
