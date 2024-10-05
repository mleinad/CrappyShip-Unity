using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour, IPuzzleBehavior 
{


    public bool interactable, pickedup;
    // Start is called before the first frame update
   public void StartPuzzle()
   {

   }

    public bool CheckCompletion()
    {

        
        return false;
    }

    public void ResetPuzzle()
    {

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
                 Player.Instance.CrosshairOn();
                interactable = false;
        }
    }

}
