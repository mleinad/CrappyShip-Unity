using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Interactable : MonoBehaviour
{
    bool interactable = false;
    bool isOn;
    bool triggered;

    [SerializeField]
    Animator animator;

    public bool repeat;
    bool beenTriggerd;
    void Awake(){
        isOn = false;
        triggered = false;
    }
    void OnTriggerEnter(Collider other){
        if(other.GetComponent<CharacterController>()){
            interactable = true;
            isOn = true;
        Debug.Log("entered");
        }
    }

    
    void OnTriggerExit(Collider other){
        if(other.GetComponent<CharacterController>()){
            interactable = false;
            isOn = true;
            triggered = false;
            Player.Instance.MessageOn(false);
        }
    }


    void Update()
    {
        if (!GetComponent<Collider>().enabled)
        {
            isOn = false;
        }

        if(interactable)
        {

            if(Input.GetKeyDown(KeyCode.E))
            {   
                beenTriggerd = true;
                Animate();
                isOn = false;
                triggered = true;
            }

            Player.Instance.MessageOn(isOn);
        }
    }

    public bool WasTriggered()=> triggered;

    public void SetTrigger(bool b) => triggered = b;

    void Animate()
    {
        if(animator!=null)
        {

            animator.SetBool("interaction", beenTriggerd);
            beenTriggerd = false;
        } 
    }
}
