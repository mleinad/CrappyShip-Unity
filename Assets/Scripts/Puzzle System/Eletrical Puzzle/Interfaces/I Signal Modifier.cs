using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISignalModifier : IEletricalComponent
{
    public int GetOutput(string context, int signal);
    public void SetSignal(int signal);
}
