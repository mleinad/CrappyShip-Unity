using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAction : MonoBehaviour   //possibly play animation action
{
    // Start is called before the first frame update
    private static readonly int Triggered = Animator.StringToHash("triggered");
    private static readonly int Interaction = Animator.StringToHash("interaction");

    
    [SerializeField]
    private Animator animator;
    [SerializeField]
    Interactable interactable;
    
    public bool timed;
    public float timer;
    bool beenTriggerd;
    void Awake()
    {
        beenTriggerd = false; 
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        EventManager.Instance.onTriggerInteraction += Perform;   
    }
    


    private void Perform(Interactable obj)
    {
        if (obj == interactable)
        {
             beenTriggerd = !beenTriggerd;
            animator.SetBool(Interaction, beenTriggerd);
            animator.SetTrigger(Triggered);
        }
    }
    
    IEnumerator DelayedAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger(Triggered);
        animator.SetBool(Interaction, !beenTriggerd);
    }
    
}
