using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    bool interactable = false;
    bool isOn;
    bool triggered;

    [SerializeField]
    Animator animator;

    public bool repeat;
    public bool timed;
    bool beenTriggerd;
    void Awake(){
        isOn = false;
        triggered = false;
    }
    void OnTriggerEnter(Collider other){
        if(other.GetComponent<CharacterController>()){
            interactable = true;
            isOn = true;
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

        if(repeat)
        {
            if(triggered)
            {
                return; //instead of this.enabled = false
            }
        }
            if (!GetComponent<Collider>().enabled)
            {
                isOn = false;
            }

            if(interactable)
            {

                if(Input.GetKeyDown(KeyCode.E))
                {   
                    beenTriggerd = !beenTriggerd;
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
        if(animator)
        {
            animator.SetBool("interaction", beenTriggerd);
        } 
    }
}
