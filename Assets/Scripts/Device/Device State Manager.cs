using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class DeviceStateManager : MonoBehaviour
{

    DeviceBaseState current_state;
    public DeviceOnHandState onHand_state = new DeviceOnHandState();
    public DeviceFullScreenState deviceFullScreen_state = new DeviceFullScreenState();
    public DeviceStoredState deviceStored_state = new DeviceStoredState();

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        current_state = deviceStored_state;

        current_state.EnterState(this);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        current_state.UpdateState(this);
    }

    public void SwitchState(DeviceBaseState state){

        current_state.ExitState(this);
        current_state = state;
        state.EnterState(this);
    }

}
