using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class Cable : MonoBehaviour, IEletricalComponent
{


   [SerializeField]
   private int signal = 0;


    [SerializeField]
    string t;
    Dictionary<IEletricalComponent, string> connection_type;

   public int GetSignal()=>signal;

    void Start(){
        connection_type = new Dictionary<IEletricalComponent, string>();
    }
    
    void OnTriggerEnter(Collider other)
    {   
        string type;
        IEletricalComponent electricalComponent;
        if (other.name.Contains("In"))
        {
            electricalComponent = other.GetComponentInParent<IEletricalComponent>();
            type = other.name;
        }
        else if (other.name.Contains("Out"))
        {
            electricalComponent = other.GetComponentInParent<IEletricalComponent>();
            type = other.name;
        }
        else
        {
            electricalComponent = other.GetComponent<IEletricalComponent>();
            if(electricalComponent is not ISignalModifier)
            {
            type = "I/O";
            }else return;
        }

        if (electricalComponent == null)
            return;
    
        if(!connection_type.ContainsKey(electricalComponent))
        {
        t= type;
        connection_type.Add(electricalComponent, type);
        }

    }

    //Dictionary Management
   void OnTriggerStay(Collider other)
   {
     /*if(other.name == "In")
        {
            Debug.Log(other.transform.parent.name);
        }*/
       
   }
    void OnTriggerExit(Collider other)
    {
            
        IEletricalComponent electricalComponent;
        if (other.name.Contains("In"))
        {
            electricalComponent = other.GetComponentInParent<IEletricalComponent>();
        }
        else if (other.name.Contains("Out"))
        {
            electricalComponent = other.GetComponentInParent<IEletricalComponent>();
        }
        else
        {
            electricalComponent = other.GetComponent<IEletricalComponent>();
        }

        if (electricalComponent == null)
            return;
        
        if(connection_type.ContainsKey(electricalComponent))
        {
            connection_type.Remove(electricalComponent);
            signal = 0;
            t = " ";
        }
    }

    void Update()
    {

        foreach(KeyValuePair<IEletricalComponent, string> con in connection_type)
        {
            if(con.Key is Battery battery)
            {
                signal = battery.GetSignal();
            }
            else if (con.Key is Cable cable)
            {
                if(cable.GetSignal()>0){
                    signal = cable.GetSignal();
                }
            }
            else if (con.Key is ISignalModifier signalModifier)
            {
                signal = signalModifier.GetOutput(con.Value ,signal); 
                //Debug.Log(transform.name + "input->" + con.Value);
            }

        }


    }
}
