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

    private InteractableAction interactableAction;
    private Terminal terminal;
    private AudioSource audioSource;
    void Awake(){
        isOn = false;
        triggered = false;

        interactableAction = GetComponent<InteractableAction>();
        terminal = GetComponent<Terminal>();
        audioSource = GetComponent<AudioSource>();
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
                    if (runtimeAnimatorController.name == "AnimatorOne")
                    {
                        audioSource.clip = Resources.Load<AudioClip>("SoundOne");
                    }
                    else if (runtimeAnimatorController.name == "AnimatorTwo")
                    {
                        audioSource.clip = Resources.Load<AudioClip>("SoundTwo");
                    }
                    else
                    {
                        audioSource.clip = Resources.Load<AudioClip>("SoundThree");
                    }
                    audioSource.Play();
                }
            }
        }
        else if (terminal != null)
        {
            // Play terminal interaction sound
            audioSource.clip = Resources.Load<AudioClip>("TerminalSound");
            audioSource.Play();
        }
        else
        {
            // No action for objects without InteractableAction or Terminal scripts
            Debug.Log($"No sound assigned for {gameObject.name} as it lacks relevant scripts.");
        }
    }
}
