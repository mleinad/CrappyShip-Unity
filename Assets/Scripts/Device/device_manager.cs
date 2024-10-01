using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class device_manager : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public Transform device;


     AnimatorClipInfo[] clip_info;
    bool device_pos1 = false, device_pos2 = false, device_pos0 = true;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        InputControl();

    }


    void InputControl()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            device_pos1 = !device_pos1;
            
            animator.SetBool("Position1", device_pos1);
        }

        if(Input.GetKeyDown(KeyCode.Tab)){
            device_pos2 = !device_pos2;
            
            animator.SetBool("Position2", device_pos2);
        }

    }


}
