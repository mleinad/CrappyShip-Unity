using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IEletricalComponent
{
    [SerializeField]
    private int Signal;

    public List<IEletricalComponent> GetAdjacencies()
    {
        throw new System.NotImplementedException();
    }

    public int GetSignal() => Signal; 

}
