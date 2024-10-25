using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISignalModifier
{
    public int GetOutput(ColliderIO input);
    public void SetSignal(int signal);

}
    