using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // Static instance for Singleton
    public static Player Instance { get; private set; }
    
    [SerializeField]
    private GameObject crosshair1, crosshair2;

    CharacterController controller;
    Vector3 movement_vec = Vector3.zero;

    Animator playerAnim;

    private bool is_rotation_locked = false;    

#region Player Variables 
    public float walking_speed = 7.5f;
    public float running_speed = 11.5f;
    public float jump_speed = 8.0f;
    public float gravity = 20.0f;
    public float look_speed = 2.0f;
    public float look_X_limit = 45.0f;
#endregion

    public Camera playerCamera;

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

        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

        bool isWalking = movement_vec.x != 0 || movement_vec.z != 0;
        playerAnim.SetBool("isWalking", isWalking);

        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            movement_vec.y = jump_speed;
            playerAnim.SetBool("isJumping",true);
        }
        else
        {
            movement_vec.y = _movementY;
            playerAnim.SetBool("isJumping", false);
        }

        if (!controller.isGrounded)
        {
            movement_vec.y -= gravity * Time.deltaTime;
        }

        controller.Move(movement_vec * Time.deltaTime);

        // Camera rotation
        if(!is_rotation_locked) MoveCamera();
    }


    private void MoveCamera(){

        rotation_X += -Input.GetAxis("Mouse Y") * look_speed;
        rotation_X = Mathf.Clamp(rotation_X, -look_X_limit, look_X_limit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation_X, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * look_speed, 0);
    }



    public void LockCamera(bool state) => is_rotation_locked = state;


    public Transform GetMainCameraTransform(){
        return playerCamera.transform;
    }



#region UI
    
    public void CrosshairOn(){
        
        crosshair1.SetActive(false);
        crosshair2.SetActive(true);
    }
    public void CrosshairOff(){

        crosshair1.SetActive(true);
        crosshair2.SetActive(false);
    }

#endregion
}
