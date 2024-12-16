using UnityEngine;
using UnityEngine.UI;

public class HintMediaState : HintBaseState
{
    private RawImage image;
    public override void EnterState(HintsUIManager context)
    {
        context.mediaPanel.SetActive(true);
        image = context.mediaPanel.transform.GetChild(0).GetComponent<RawImage>();
        context.BackdropState(0.1f, true);
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
        Hide(context);
        context.BackdropState(0.1f, false);
    }

    public override void Hide(HintsUIManager context)
    {
        context.mediaPanel.SetActive(false);
    }

    public void SetImage(Texture texture)
    {
        image.texture = texture;
    }
}
