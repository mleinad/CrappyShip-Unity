using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{

    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maxWalkSpeed = 0.5f;
    public float maxRunSpeed = 2.0f;

    int VelocityZHash;
    int VelocityXHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        VelocityXHash = Animator.StringToHash("Velocity X");
        VelocityZHash = Animator.StringToHash("Velocity Z");
    }
    void changeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed,bool backwardPressed, bool runPressed, float currentMaxSpeed)
    {
        if (forwardPressed && velocityZ < currentMaxSpeed)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        if (leftPressed && velocityX > -currentMaxSpeed)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        if (rightPressed && velocityX < currentMaxSpeed)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
        if (backwardPressed && velocityZ > -currentMaxSpeed)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        if (!backwardPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
       
    }
    void lockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed,bool backwardPressed, bool runPressed, float currentMaxSpeed)
    {
        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }
        if (!forwardPressed && !backwardPressed && velocityZ != 0.0f &&(velocityZ>-0.05f && velocityZ <0.05f))
        {
            velocityZ = 0.0f;
        }
        //forward lock
        if (forwardPressed && runPressed && velocityZ > currentMaxSpeed)
        {
            velocityZ = currentMaxSpeed;
        }
        else if (forwardPressed && velocityZ > currentMaxSpeed)
        {
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ > currentMaxSpeed && velocityZ < (currentMaxSpeed + 0.05f))
            {
                velocityZ = currentMaxSpeed;
            }
        }
        else if (forwardPressed && velocityZ < currentMaxSpeed && velocityZ > (currentMaxSpeed - 0.05f))
        {
            velocityZ = currentMaxSpeed;
        }
        //back lock
        if (backwardPressed && runPressed && velocityZ < -currentMaxSpeed)
        {
            velocityZ = -currentMaxSpeed;
        }
        else if (backwardPressed && velocityZ < -currentMaxSpeed)
        {
            velocityZ += Time.deltaTime * deceleration;
            if (velocityZ < -currentMaxSpeed && velocityZ > (-currentMaxSpeed - 0.05f))
            {
                velocityZ = -currentMaxSpeed;
            }
        }
        else if (backwardPressed && velocityZ > -currentMaxSpeed && velocityZ < (-currentMaxSpeed + 0.05f))
        {
            velocityZ = -currentMaxSpeed;
        }
        //left lock
        if (leftPressed && runPressed && velocityX < -currentMaxSpeed)
        {
            velocityX = -currentMaxSpeed;
        }
        else if (leftPressed && velocityX < -currentMaxSpeed)
        {
            velocityX += Time.deltaTime * deceleration;
            if (velocityX < -currentMaxSpeed && velocityX > (-currentMaxSpeed - 0.05f))
            {
                velocityX = -currentMaxSpeed;
            }
        }
        else if (leftPressed && velocityX > -currentMaxSpeed && velocityX < (-currentMaxSpeed + 0.05f))
        {
            velocityX = -currentMaxSpeed;
        }

        if (rightPressed && runPressed && velocityX > currentMaxSpeed)
        {
            velocityX = currentMaxSpeed;
        }
        else if (rightPressed && velocityX > currentMaxSpeed)
        {
            velocityX -= Time.deltaTime * deceleration;
            if (velocityX > currentMaxSpeed && velocityX < (currentMaxSpeed + 0.05f))
            {
                velocityX = currentMaxSpeed;
            }
        }
        else if (rightPressed && velocityX < currentMaxSpeed && velocityX > (currentMaxSpeed - 0.05f))
        {
            velocityX = currentMaxSpeed;
        }
    }
    void normalizeDiagonalMovement(ref float velocityX, ref float velocityZ, float currentMaxSpeed)
    {
        Vector3 inputDirection = new Vector3(velocityX, 0, velocityZ);

        // If the player is moving diagonally, normalize the direction to prevent speed boost
        if (inputDirection.magnitude > 1)
        {
            inputDirection.Normalize();
            velocityX = inputDirection.x * currentMaxSpeed;
            velocityZ = inputDirection.z * currentMaxSpeed;
        }
    }
    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool backwardPressed = Input.GetKey(KeyCode.S);

        float currentMaxSpeed = runPressed ? maxRunSpeed : maxWalkSpeed;

        changeVelocity(forwardPressed, leftPressed, rightPressed,backwardPressed,runPressed,currentMaxSpeed);
        normalizeDiagonalMovement(ref velocityX, ref velocityZ, currentMaxSpeed);
        lockOrResetVelocity(forwardPressed,leftPressed,rightPressed,backwardPressed,runPressed,currentMaxSpeed);
       

      



        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }
}
