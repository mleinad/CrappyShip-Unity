using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeviceBaseState
{

    public abstract void EnterState(DeviceStateManager context);

    public abstract void UpdateState(DeviceStateManager context);

    public abstract void ExitState(DeviceStateManager context);

}
