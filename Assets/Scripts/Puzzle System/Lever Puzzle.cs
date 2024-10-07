using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour, IPuzzleBehavior 
{


    public bool interactable;

    [SerializeField]
    Animator animator;

    [SerializeField]
    private bool correct_position;
    private bool curent_position;
    private bool state;
    // Start is called before the first frame update
   public void StartPuzzle()
   {

   }

    void Awake()
    {

        animator = GetComponent<Animator>();

    }
    void Update(){


        if(Input.GetMouseButtonDown(0)){
                
                if(interactable) FlickLever();

                if(curent_position = correct_position){
                    state = true;
                }
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
    void FlickLever(){
        
        curent_position =!curent_position;
        animator.SetBool("position", curent_position);

    }
    public bool CheckCompletion()
    {
  
        return state;
    }

    public void ResetPuzzle()
    {
        state = false;
    }  



   

}
