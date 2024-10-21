using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Interactable : MonoBehaviour
{
    bool interactable = false;
    bool isOn = true;
    

    void OnTriggerEnter(Collider other){
        if(other.GetComponent<CharacterController>()){
            interactable = true;
        }
    }

    
    void OnTriggerExit(Collider other){
        if(other.GetComponent<CharacterController>()){
            interactable = false;
            isOn = true;
            Player.Instance.MessageOn(false);
        }
    }


    void Update()
    {

        if(interactable)
        {

            if(Input.GetKeyDown(KeyCode.E)){
                isOn = false;
            }
        
        Player.Instance.MessageOn(isOn);
        }
    }

}
