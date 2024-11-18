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
    
    public bool timed = false;
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
        beenTriggerd = !beenTriggerd;
        if (obj == interactable)
        {
            animator.SetBool(Interaction, beenTriggerd);
            animator.SetTrigger(Triggered);
            if (timed)
            {
                StartCoroutine(DelayedAnimation(timer));
            }

        }
    }
    
    IEnumerator DelayedAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger(Triggered);
        animator.SetBool(Interaction, !beenTriggerd);
    }
    
}
