using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour   //possibly play animation action
{
    // Start is called before the first frame update
   
   [SerializeField]
    private Animator animator;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        EventManager.Instance.onTriggerOpenDoor += Perform;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Perform(int id)
    {
        id = 1;
        animator.SetTrigger("open");
    }
    
}
