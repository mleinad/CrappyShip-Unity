using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTerminal : MonoBehaviour
{

    [SerializeField]
    CanvasGroup canvasGroup;

    [SerializeField]
    DeviceStateManager deviceStateManager;

    // Start is called before the first frame update
    void Start()
    {
        EnableTerminal(false);   
    }

    // Update is called once per frame
    void Update()
    {
        if(deviceStateManager.GetCurrentDeviceState() is DeviceFullScreenState)
        {
            EnableTerminal(true);
        }
        else
        {
            EnableTerminal(false);
        }
    }

    public void EnableTerminal(bool state)
    {
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }
}
