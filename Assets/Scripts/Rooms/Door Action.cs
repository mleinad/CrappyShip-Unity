using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour, IActions
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Perform()
    {
        animator.SetTrigger("open");
    }
    
}
