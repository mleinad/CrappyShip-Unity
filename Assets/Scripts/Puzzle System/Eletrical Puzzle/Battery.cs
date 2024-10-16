using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IEletricalComponent
{
    [SerializeField]
    private int Signal;
    public int GetSignal() => Signal; 

}
