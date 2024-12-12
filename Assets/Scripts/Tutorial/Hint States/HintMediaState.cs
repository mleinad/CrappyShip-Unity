using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintMediaState : HintBaseState
{
    [SerializeField]
    GameObject mediaPanel;
    public override void EnterState(HintsUIManager context)
    {
        mediaPanel.SetActive(true);
    }

    public override void UpdateState(HintsUIManager context)
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            context.SwitchState(context.offState);
        }
    }

    public override void ExitState(HintsUIManager context)
    {
        Hide();
    }

    public override void Hide()
    {
        mediaPanel.SetActive(false);
    }
}
