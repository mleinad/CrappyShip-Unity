using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISignalModifier
{
    public int GetOutput();
    public void SetSignal(Dictionary<IEletricalComponent, ColliderIO> pair);

}
    