using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISignalModifier //add IElectricalComponent implementation maybe
{
    public int GetOutput(ColliderIO input, int signal);
    public void SetSignal(int signal);

}
