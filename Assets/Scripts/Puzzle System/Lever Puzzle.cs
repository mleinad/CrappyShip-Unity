using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour, IPuzzleBehavior 
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



   

}
