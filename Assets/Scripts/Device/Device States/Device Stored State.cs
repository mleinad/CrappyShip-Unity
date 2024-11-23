using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceStoredState : DeviceBaseState
{
    string trigger = "position0";
     public override void EnterState(DeviceStateManager context){
        context.animator.SetTrigger(trigger);

    }

    public override void UpdateState(DeviceStateManager context){

        if(Input.GetKeyDown(KeyCode.Tab)){
            context.SwitchState(context.deviceFullScreen_state);
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            context.SwitchState(context.onHand_state);
        }
    }

    public override void ExitState(DeviceStateManager context){
    }
}

