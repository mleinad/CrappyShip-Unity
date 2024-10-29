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

            if(Input.GetKeyDown(KeyCode.E)){
                isOn = false;
                triggered = true;
            }

             Player.Instance.MessageOn(isOn);
        }
    }

    public bool WasTriggered()=> triggered;

}
