using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragNDropBaseState
{
    public abstract void EnterState(DragNDrop context);

    public abstract void UpdateState(DragNDrop context);

    public abstract void ExitState(DragNDrop context);

}
