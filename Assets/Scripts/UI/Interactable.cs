using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static Unity.Barracuda.TextureAsTensorData;

public class Interactable : MonoBehaviour
{
    private static readonly int Triggered = Animator.StringToHash("triggered");
    private static readonly int Interaction = Animator.StringToHash("interaction");
    
    bool interactable = false;
    bool isOn;
    bool triggered;
    
    public bool repeat;
    bool beenTriggerd;

    private InteractableAction interactableAction;
    private Terminal terminal;
    private AudioSource audioSource;
    void Awake(){
        isOn = false;
        triggered = false;

        interactableAction = GetComponent<InteractableAction>();
        terminal = GetComponent<Terminal>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; // Prevent audio from playing immediately
        }
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
        PlaySound();
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
    private void PlaySound()
    {
        if (audioSource == null) return;

        if (interactableAction != null)
        {
            // Determine the sound based on the animator controller
            var animator = interactableAction.GetComponent<Animator>();
            if (animator != null)
            {
                var runtimeAnimatorController = animator.runtimeAnimatorController;
                if (runtimeAnimatorController != null)
                {
                    if (runtimeAnimatorController.name == "SM_Bld_Door_Single_03 (1)")
                    {
                        if (interactableAction.beenTriggerd)
                        {
                            audioSource.clip = Resources.Load<AudioClip>("SoundDoor1");
                        }
                        
                    }
                    else if (runtimeAnimatorController.name == "door3")
                    {
                        if (interactableAction.beenTriggerd) 
                        {
                            audioSource.clip = Resources.Load<AudioClip>("SoundDoor2");
                        } 
                        
                    }
                    else if(runtimeAnimatorController.name =="Locker")
                    {
                        if(animator.GetBool("interaction") == true)
                        {
                            audioSource.clip = Resources.Load<AudioClip>("SoundLocker");
                        }else if(animator.GetBool("interaction") == false)
                        {
                            audioSource.clip = Resources.Load<AudioClip>("SoundLockerClose");
                        }    
                        
                    }
                    audioSource.Play();
                }
            }
        }
        else if (terminal != null)
        {
            // Play terminal interaction sound
            if (WasTriggered())
            {
                audioSource.clip = Resources.Load<AudioClip>("TerminalSound");
                if (audioSource.clip == null)
                {
                    Debug.LogError("Failed to load SoundOne. Check the file name and ensure it's in the Resources folder.");
                }
                audioSource.Play();
            }
        }
        else
        {
        }
    }
}
