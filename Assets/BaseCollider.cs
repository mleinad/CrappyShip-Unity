using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollider : MonoBehaviour
{

    IEletricalComponent eletricalComponent;
    public IEletricalComponent GetEletricalComponent() => eletricalComponent;

   void OnTriggerEnter(Collider collider)
   {
        IEletricalComponent ec = collider.GetComponent<IEletricalComponent>();

        if(ec==null) return;
        eletricalComponent = ec;
   }

   void OnTriggerExit(Collider collider)
   {
        eletricalComponent = null;
   }
}
