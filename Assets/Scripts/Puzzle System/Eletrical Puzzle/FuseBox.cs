using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class FuseBox : MonoBehaviour, IPuzzleComponent, IEletricalComponent
{
    bool state= false;
    public int signal;
    public float roation_speed;
    public int signal_needed;
    public bool CheckCompletion()=>state;

    public int GetSignal()
    {
       return signal;
    }

    public void ResetPuzzle()=> state = false;

    void Update()
    {
            if(signal > signal_needed) state = true;
            else state = false;    
    
        if(state)
        {

            transform.GetChild(0).Rotate(0, 0, roation_speed * Time.deltaTime); 

        }
    }


    void OnTriggerStay(Collider other)
    {
        ColliderIO col;
        IEletricalComponent electricalComponent;
        col = other.GetComponent<ColliderIO>();
        if(col==null) return;
        electricalComponent = col.GetEletricalComponent();
        
        if(electricalComponent==null) return;
        if(electricalComponent.GetSignal()>0)
        {
        signal = electricalComponent.GetSignal();
        }
       // Debug.Log(other.name + " " + electricalComponent.GetSignal());
    }
    void OnTriggerExit()
    {
        signal =0;
    }

    public List<IEletricalComponent> GetAdjacencies()
    {
        throw new System.NotImplementedException();
    }
}
