using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // Static instance for Singleton
    public static Player Instance { get; private set; }
    public Transform right_hand;

    public TwoBoneIKConstraint right_arm;
    public TwoBoneIKConstraint left_arm;


    [SerializeField]
    private GameObject crosshair1, crosshair2, message;

    CharacterController controller;
    Vector3 movement_vec = Vector3.zero;

    float velocityZ = 0.0f;
    float velocityX = 0.0f;

    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maxWalkSpeed = 0.5f;
    public float maxRunSpeed = 1.2f;
    bool forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed;



    int velocityZHash;
    int velocityXHash;

    Animator playerAnim;

    private bool is_rotation_locked = false;    
     private bool movement_locked = false;    

#region Player Variables 
    public float walking_speed = 3.5f;
    public float running_speed = 6.0f;
    public float jump_speed = 8.0f;
    public float gravity = 20.0f;
    public float look_speed = 2.0f;
    public float look_X_limit = 45.0f;
#endregion

    public Transform playerCamera;

    private float rotation_X = 0;



    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Implement Singleton Pattern
        if (Instance == null)
        {
            Instance = this;  // Set this instance as the singleton instance
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // Destroy the extra instance to ensure there is only one
        }

        DontDestroyOnLoad(gameObject);  // Optional: Persist the singleton across scenes
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();

        velocityXHash = Animator.StringToHash("Velocity X");
        velocityZHash = Animator.StringToHash("Velocity Z");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        left_arm.weight =0;
        right_arm.weight =0;
    }

    // Update is called once per frame
    void Update()
    {
        if(movement_locked) return;
        HandleInput();
        HandleMovementAndAnimations();
        if(is_rotation_locked) return;
        HandleCameraRotation();

    }
    void HandleMovementAndAnimations()
    {
        float currentMaxSpeed = runPressed ? maxRunSpeed : maxWalkSpeed;

        // Calculate velocity changes for animations
        ChangeVelocity(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed, currentMaxSpeed);
        NormalizeDiagonalMovement(ref velocityX, ref velocityZ, currentMaxSpeed);
        LockOrResetVelocity(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed, currentMaxSpeed);

        // Set the animator parameters
        playerAnim.SetFloat(velocityXHash, velocityX);
        playerAnim.SetFloat(velocityZHash, velocityZ);

        // Physical movement based on the calculated velocity
        Vector3 move = transform.TransformDirection(new Vector3(velocityX, 0, velocityZ));
        movement_vec = move * (runPressed ? running_speed : walking_speed);

        // Apply gravity
        if (!controller.isGrounded)
        {
            movement_vec.y -= gravity * Time.deltaTime;
        }

        // Move the character
        controller.Move(movement_vec * Time.deltaTime);
    }

    void HandleCameraRotation()
    {
        rotation_X += -Input.GetAxis("Mouse Y") * look_speed;
        rotation_X = Mathf.Clamp(rotation_X, -look_X_limit, look_X_limit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation_X, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * look_speed, 0);
    }
    void ChangeVelocity(bool forwardPressed, bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxSpeed)
    {
        if (forwardPressed && velocityZ < currentMaxSpeed)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        if (backwardPressed && velocityZ > -currentMaxSpeed)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        if (leftPressed && velocityX > -currentMaxSpeed)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        if (rightPressed && velocityX < currentMaxSpeed)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        

        // Decrease velocity when keys are not pressed
        if (!forwardPressed && !backwardPressed && velocityZ != 0)
        {
            velocityZ = Mathf.MoveTowards(velocityZ, 0, Time.deltaTime * deceleration);
        }
        if (!leftPressed && !rightPressed && velocityX != 0)
        {
            velocityX = Mathf.MoveTowards(velocityX, 0, Time.deltaTime * deceleration);
        }
    }

    // Prevents diagonal movement from being faster than forward or sideways movement
    void NormalizeDiagonalMovement(ref float velocityX, ref float velocityZ, float currentMaxSpeed)
    {
        Vector3 inputDirection = new Vector3(velocityX, 0, velocityZ);

        if (inputDirection.magnitude > 1)
        {
            inputDirection.Normalize();
            velocityX = inputDirection.x * currentMaxSpeed;
            velocityZ = inputDirection.z * currentMaxSpeed;
        }
    }

    // Locks velocity values for consistent transitions
    void LockOrResetVelocity(bool forwardPressed, bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxSpeed)
    {
        // Lock forward or backward velocity
        if (forwardPressed && runPressed && velocityZ > currentMaxSpeed)
        {
            velocityZ = currentMaxSpeed;
        }
        else if (backwardPressed && velocityZ < -currentMaxSpeed)
        {
            velocityZ = -currentMaxSpeed;
        }

        // Lock side velocities
        if (leftPressed && velocityX < -currentMaxSpeed)
        {
            velocityX = -currentMaxSpeed;
        }
        if (rightPressed && velocityX > currentMaxSpeed)
        {
            velocityX = currentMaxSpeed;
        }
    }
    void HandleInput()
    {
        forwardPressed = Input.GetKey(KeyCode.W);
        backwardPressed = Input.GetKey(KeyCode.S);
        leftPressed = Input.GetKey(KeyCode.A);
        rightPressed = Input.GetKey(KeyCode.D);
        runPressed = Input.GetKey(KeyCode.LeftShift);
    }

    private void MoveCamera(){

        rotation_X += -Input.GetAxis("Mouse Y") * look_speed;
        rotation_X = Mathf.Clamp(rotation_X, -look_X_limit, look_X_limit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation_X, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * look_speed, 0);
    }



    public void LockCamera(bool state) => is_rotation_locked = state;
    public void LockMovement(bool locked) => movement_locked = locked;
    public void SetCameraSpeed(float speed)=> look_speed = speed;

    public Transform GetMainCameraTransform(){
        return playerCamera.transform;
    }

    public Vector3 GetPlayerPositon() => transform.position;
    public Transform GetPlayerRightHand()=> right_hand;


    public void RaiseLeftArm(float y)
    {
        DOTween.To(()=> left_arm.weight, x => left_arm.weight = x, y, 1f);
    }

    public void RaiseRightArm(float y)
    {
         DOTween.To(()=> left_arm.weight, x => left_arm.weight = x, y, 1f);
    }



#region UI
    
    public void CrosshairOn(){
        
        crosshair1.SetActive(false);
        crosshair2.SetActive(true);
     //   Debug.Log("crosshair on");
    }
    public void CrosshairOff(){


        crosshair1.SetActive(true);
        crosshair2.SetActive(false);
       // Debug.Log("crosshair off");
    }


    public void MessageOn(bool state)
    {
        message.SetActive(state);
    }

#endregion
}
