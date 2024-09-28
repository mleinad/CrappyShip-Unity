using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(CharacterController))]

public class player_controller : MonoBehaviour
{
    // Start is called before the first frame update

    CharacterController controller;
    Vector3 movement_vec = Vector3.zero;

    public float walking_speed = 7.5f;
    public float running_speed = 11.5f;
    public float jump_speed = 8.0f;
    public float gravity = 20.0f;
    public float look_speed = 2.0f;
    public float look_X_limit = 45.0f;

    public Camera playerCamera;

    float rotation_X = 0;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool is_running = Input.GetKey(KeyCode.LeftShift);

        float speed_X = Input.GetAxis("Vertical") * (is_running ? running_speed : walking_speed);          
        float speed_Y = Input.GetAxis("Horizontal") * (is_running ? running_speed : walking_speed);
        float _movementY = movement_vec.y;

        movement_vec = (forward * speed_X) + (right * speed_Y);


        if(Input.GetButton("Jump") && controller.isGrounded)
        {
            movement_vec.y = jump_speed;
        }
        else
        {
            movement_vec.y = _movementY;
        }


        if(!controller.isGrounded)
        {
            movement_vec.y -= gravity* Time.deltaTime;
        }
        
        
        controller.Move(movement_vec * Time.deltaTime);


        rotation_X += -Input.GetAxis("Mouse Y") * look_speed;
        rotation_X = Mathf.Clamp(rotation_X, -look_X_limit, look_X_limit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation_X, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * look_speed, 0);
    }

}

