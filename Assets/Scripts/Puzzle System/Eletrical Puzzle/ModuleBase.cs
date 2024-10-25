using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ModuleBase : MonoBehaviour
{

    int input1, input2, input3, input4;
    public string component_name;
    public List<string> cable_names;

    ISignalModifier signalModifier;

    List<BaseCollider> baseColliders;

    void Awake(){
         foreach(Transform child in transform){
         }
    }


    public void Hover()
    {



    }

    void Update()
    {
        //CheckComponent();
        DrawVectors();

        if(signalModifier==null) component_name = "no attachements";
        else component_name = signalModifier.ToString();
    
    
        foreach(BaseCollider col in baseColliders)
        {   

            IEletricalComponent ec = col.GetEletricalComponent();
            if(ec is Cable){
                cable_names.Add(ec.ToString() + " " + col.ToString());
            }
            
        }
    
    }

    void DrawVectors()  //also updates them
    {

        float vectorLength = 2.0f;
        
        Vector3 front = transform.position + transform.forward * vectorLength;
        Vector3 back = transform.position - transform.forward * vectorLength;
        Vector3 right = transform.position + transform.right * vectorLength;
        Vector3 left = transform.position - transform.right * vectorLength;

        Debug.DrawLine(transform.position, front, Color.green);  // Front (green)
        Debug.DrawLine(transform.position, back, Color.red);     // Back (red)
        Debug.DrawLine(transform.position, right, Color.blue);   // Right (blue)
        Debug.DrawLine(transform.position, left, Color.yellow);  // Left (yellow)

    }
    
    public void SetComponent(ISignalModifier modifier){

      signalModifier = modifier;

    }
    public List<Vector3> GetRotationAngles()
    {
    Vector3 front = transform.forward;
    Vector3 back = -transform.forward;
    Vector3 right = transform.right;
    Vector3 left = -transform.right;

    List<Vector3> vectorList = new List<Vector3>()
    {
        front,
        back,
        right,
        left
    };

    return vectorList; 
}



    void OnTriggerEnter(Collider other)
    {
        Cable cable = other.GetComponent<Cable>();
        if(cable==null) return;

        GetInputAngles(other.transform);
    }
    
    
    void GetInputAngles(Transform t)
    {
        List<Vector3> vectorList = GetRotationAngles();
        int i=1;
        foreach(Vector3 vec in vectorList)
        {
        float dot = Vector3.Dot(t.forward, vec);
        Debug.Log("dot " + i + " ->" + dot);
        i++;
        }
    
    }

}
