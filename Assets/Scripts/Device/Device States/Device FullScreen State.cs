using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceFullScreenState : DeviceBaseState
{
    string trigger = "position2";
    public override void EnterState(DeviceStateManager context)
    {
        context.animator.SetTrigger(trigger);

         Cursor.visible = true;
         Cursor.lockState = CursorLockMode.None;
         Player.Instance.LockCamera(true);

    }

    public override void UpdateState(DeviceStateManager context){
        if(Input.GetKeyDown(KeyCode.Tab)){
            context.SwitchState(context.deviceStored_state);
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            context.SwitchState(context.onHand_state);
        }


        //update method of the state
    }

      public override void ExitState(DeviceStateManager context){

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Player.Instance.LockCamera(false);

    }
}
