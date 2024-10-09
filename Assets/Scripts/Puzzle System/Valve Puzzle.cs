using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ValvePuzzle : MonoBehaviour, IPuzzleBehavior
{
    // Start is called before the first frame update


    public bool interactable, pickedup;
    private bool state = false;
    public float rotationSpeed = 0f; // Speed at which the valve rotates

    
    //for debug only 
    public Vector2 C;
    public Vector3 A, B;


    public float r;
    public Transform valve_transform;


    private Vector3 collision_point;
    private Vector3 change;



    void Start()
    {
        //change = rotation_pivot.position;   
    }

   public void StartPuzzle()
    {

    }


    public bool CheckCompletion()
    {


        return state;
    }

    public void ResetPuzzle()
    {

    }  



    // Update is called once per frame
    void Update()
    {
        if(interactable)
        {

            if(Input.GetMouseButton(0))
            {
                
                HandleRotation();


            }
        }
        if(Input.GetMouseButtonUp(0)) Player.Instance.SetCameraSpeed(2.0f); //maybe bad for performance

    }

    void HandleRotation(){
        Player.Instance.SetCameraSpeed(1f);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 current = collision_point;      



        C.x = valve_transform.position.x; C.y = valve_transform.position.z;


        Vector3 center_to_mouseA = current - valve_transform.position; 
        Vector3 center_to_mouseB = change - valve_transform.position;

        float rotation_angle = Vector3.SignedAngle(center_to_mouseA, center_to_mouseB, Vector3.forward);
        
#region debug
        r= rotation_angle;
        A = center_to_mouseA;
        B = center_to_mouseB;

        Debug.DrawLine(valve_transform.position, current, Color.red);
        Debug.DrawLine(valve_transform.position, change, Color.green);
        Debug.DrawRay(ray.origin, ray.direction*10, Color.yellow);

#endregion


        valve_transform.Rotate(0, rotation_angle ,0);
        change = current;
    }


    void _HandleRotation(){ //like unity editor (?)
            Player.Instance.SetCameraSpeed(0.2f);

                change.x =  Input.GetAxis("Mouse X");
                change.y =  Input.GetAxis("Mouse Y");

                change.Normalize();


                valve_transform.Rotate(0, -change.x*rotationSpeed * Time.deltaTime,0, Space.Self);
                valve_transform.Rotate(0, -change.y*rotationSpeed * Time.deltaTime,0, Space.Self);


                Debug.Log( "vertical -> " + change.x + " horizontal -> " + change.y);

    }
   
   
    void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("MainCamera"))
            {
                collision_point = other.ClosestPoint(transform.position);
                change = collision_point;
            }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Player.Instance.CrosshairOn();
            interactable = true;

            collision_point = other.ClosestPoint(transform.position);
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Player.Instance.CrosshairOff();
            interactable = false;
        }
    }

}
