
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // Static instance for Singleton
    public static Player Instance { get; private set; }
    public Transform right_hand;

    public TwoBoneIKConstraint right_arm;
    public TwoBoneIKConstraint left_arm;
    
    [SerializeField]
    private DeviceStateManager deviceStateManager;
    
    [SerializeField]
    private GameObject crosshair1, crosshair2, message;

    [SerializeField]
    Camera main_camera;
    CharacterController controller;
    Vector3 movement_vec = Vector3.zero;


    [SerializeField]
    private Light player_light;
        
    Animator playerAnim;

    private bool is_rotation_locked = false;
    private bool movement_locked = false;    

#region Player Variables 
    public float walking_speed = 3.5f;
    public float running_speed = 6.0f;
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
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
 


        left_arm.weight =0;
        right_arm.weight =0;
        
        player_light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(movement_locked) return;
        CharacterMovement();
        if(is_rotation_locked) return;
        HandleCameraRotation();
        HandleFocus();
    }
    void CharacterMovement()
    {
        float moveDirectionY = movement_vec.y; // Preserve vertical velocity for gravity
        float inputX = Input.GetAxis("Horizontal"); // Left/Right movement
        float inputZ = Input.GetAxis("Vertical");   // Forward/Backward movement

        // Determine movement speed (running if Shift is pressed)
        float speed = Input.GetKey(KeyCode.LeftShift) ? running_speed : walking_speed;

        // Calculate movement vector relative to player orientation
        movement_vec = transform.TransformDirection(new Vector3(inputX, 0, inputZ).normalized) * speed;

        // Apply gravity
        if (!controller.isGrounded)
        {
            moveDirectionY -= gravity * Time.deltaTime;
        }
        else
        {
            moveDirectionY = 0; // Reset gravity effect when on the ground
        }

        movement_vec.y = moveDirectionY;

        // Move the character controller
        controller.Move(movement_vec * Time.deltaTime);

        // Update animator parameters (if applicable)
        if (playerAnim != null)
        {
            playerAnim.SetFloat("Speed", movement_vec.magnitude);
        }
    
}

    void HandleCameraRotation()
    {
        rotation_X += -Input.GetAxis("Mouse Y") * look_speed;
        rotation_X = Mathf.Clamp(rotation_X, -look_X_limit, look_X_limit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation_X, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * look_speed, 0);
    }
   
  

    private void MoveCamera(){

        rotation_X += -Input.GetAxis("Mouse Y") * look_speed;
        rotation_X = Mathf.Clamp(rotation_X, -look_X_limit, look_X_limit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation_X, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * look_speed, 0);
    }

    private void HandleFocus()
    {
        if(Input.GetMouseButton(1))
        {
            DOTween.To(()=> main_camera.fieldOfView, x => main_camera.fieldOfView = x, 30, 1f);
        }else
        {
        
            DOTween.To(()=> main_camera.fieldOfView, x => main_camera.fieldOfView = x, 60, 0.2f);
        
        }
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

    public void EnablePlayerLight(bool state)
    {
        player_light.enabled = state;
    }

    public void DeviceEnabledState(bool state) => deviceStateManager.SetState(state);
    
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

    public void CrosshairFullOff()
    {
        crosshair1.SetActive(false);
        crosshair2.SetActive(false);
    }
    
    public void MessageOn(bool state)
    {
        message.SetActive(state);
    }

#endregion
}
