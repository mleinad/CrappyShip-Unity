using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEletricalComponent
{
        public int GetSignal();
        public void SetSignal(int value);
        public Dictionary<IEletricalComponent, ColliderIO> GetAdjacencies();
        public void OnChildrenTriggerExit(ColliderIO current_collider, Collider other);
        public void OnChildrenTriggerEnter(ColliderIO current_collider, Collider other);


}
