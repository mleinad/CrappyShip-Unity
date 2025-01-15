using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
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
    void Awake()
    {
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
    void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<CharacterController>())
        {
            interactable = true;
            isOn = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            interactable = false;
            isOn = true;
            triggered = false;
            Player.Instance.MessageOn(false);
        }
    }


    void Update()
    {
        if (!repeat)
        {
            if (triggered)
            {
                return; //instead of this.enabled = false
            }
        }
        if (!GetComponent<Collider>().enabled)
        {
            isOn = false;
        }

        if (interactable)
        {

            if (Input.GetKeyDown(KeyCode.E))
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
    public bool WasTriggered() => triggered;

    public void SetTrigger(bool b) => triggered = b;

    void Action()
    {
        EventManager.Instance.OnTriggerInteraction(this);
    }
    private void PlaySound()
    {
         bool soundPlayed = false;

        if (audioSource == null) return;

        audioSource.loop = false;

        if (interactableAction != null)
        {
            var animator = interactableAction.GetComponent<Animator>();
            if (animator != null)
            {
                var runtimeAnimatorController = animator.runtimeAnimatorController;
                if (runtimeAnimatorController != null)
                {
                    AudioClip clipToPlay = null;

                    // Check the runtimeAnimatorController name
                    switch (runtimeAnimatorController.name)
                    {
                        case "SM_Bld_Door_Single_03 (1)":
                            if (WasTriggered())
                            {
                                clipToPlay = Resources.Load<AudioClip>("Door1");
                                soundPlayed = true; 
                            }
                            break;

                        case "door3":
                            if (WasTriggered())
                            {
                                clipToPlay = Resources.Load<AudioClip>("Door2");
                            }
                            break;

                        case "Locker":
                            
                            if (animator.GetBool("triggered"))
                            {
                                clipToPlay = Resources.Load<AudioClip>("SoundLocker");
                                
                            }
                            break;
                    }

                    // Play the determined clip
                    if (clipToPlay != null && (!audioSource.isPlaying || audioSource.clip != clipToPlay))
                    {
                        audioSource.clip = clipToPlay;
                        audioSource.Play();
                    }
                  
                }
            }
        }
        else if (terminal != null)
        {
            if (WasTriggered())
            {
                AudioClip terminalClip = Resources.Load<AudioClip>("TerminalSound");

                if (terminalClip != null)
                {
                    if (!audioSource.isPlaying || audioSource.clip != terminalClip)
                    {
                        audioSource.clip = terminalClip;
                        audioSource.Play();
                    }
                }
                else
                {
                    Debug.LogError("Failed to load TerminalSound. Ensure the file is in the Resources folder.");
                }
            }
        }
    }
}
