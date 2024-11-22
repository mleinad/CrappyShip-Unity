using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestLight : MonoBehaviour, IEletricalComponent
{
    public int GetSignal()
    {
        throw new System.NotImplementedException();
    }

    public void SetSignal(int value)
    {
        throw new System.NotImplementedException();
    }

    public Dictionary<IEletricalComponent, ColliderIO> GetAdjacencies()
    {
        throw new System.NotImplementedException();
    }

    public void OnChildrenTriggerExit(ColliderIO current_collider, Collider other)
    {
        throw new System.NotImplementedException();
    }

    public void OnChildrenTriggerEnter(ColliderIO current_collider, Collider other)
    {
        throw new System.NotImplementedException();
    }
}
