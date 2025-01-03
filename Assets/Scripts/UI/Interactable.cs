using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    private static readonly int Triggered = Animator.StringToHash("triggered");
    private static readonly int Interaction = Animator.StringToHash("interaction");
    
    bool interactable = false;
    bool isOn;
    bool triggered;
    
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
            if(!repeat)
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
                    Action();
                    StartCoroutine(OffDelay());
                    triggered = true;
                }

                Player.Instance.MessageOn(isOn);
            }
    }

    IEnumerator OffDelay()
    {
        isOn = false;
        yield return new WaitForSeconds(1f);
        isOn = true;
    }
    public bool WasTriggered()=> triggered;

    public void SetTrigger(bool b) => triggered = b;

    void Action()
    {
        EventManager.Instance.OnTriggerInteraction(this);   
    }
    
}
