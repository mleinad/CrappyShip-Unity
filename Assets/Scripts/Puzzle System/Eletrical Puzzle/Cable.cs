using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
    Dictionary<IEletricalComponent, ColliderIO> connection_type;

   public int GetSignal()=>signal;

    void Start(){
        connection_type = new Dictionary<IEletricalComponent, ColliderIO>();
    }
    void OnTriggerEnter(Collider other)
    {   
        IEletricalComponent electricalComponent;
        ColliderIO collider;
        collider = other.GetComponent<ColliderIO>();

        if(collider!= null)
        {
            electricalComponent = collider.GetEletricalComponent();

        }else return;

        if (electricalComponent == null)
            return;
    
        if(!connection_type.ContainsKey(electricalComponent))
        {
        connection_type.Add(electricalComponent, collider);
        }

    }

    void OnTriggerExit(Collider other)
    {
            
        ColliderIO collider;
        IEletricalComponent electricalComponent;
        
        collider = other.GetComponent<ColliderIO>();

        if(collider!= null)
        {
            electricalComponent = collider.GetEletricalComponent();

        }else return;


        if (electricalComponent == null)
            return;
        
        if(connection_type.ContainsKey(electricalComponent))
        {
            if(electricalComponent is SignalEvaluator signalEvaluator) signalEvaluator.Disconnect(collider);
            connection_type.Remove(electricalComponent);
              //signal = 0;
            t = " ";
        }
    }

    void Update()
    {
        
        signal =0;
        foreach(KeyValuePair<IEletricalComponent, ColliderIO> con in connection_type)
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
                signal = signalModifier.GetOutput(con.Value, signal); 
                Debug.Log(transform.name + "input->" + con.Value);
            }

        }
        


    }


}
