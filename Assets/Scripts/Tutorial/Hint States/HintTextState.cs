using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintTextState : HintBaseState
{
    private string content;
    private string instruction = "press esc to hide hint";
    
    [SerializeField]
    private TMP_Text hintText;
    [SerializeField]
    private TMP_Text instructionText;
    [SerializeField]
    private Image backPanel;
    
    public override void EnterState(HintsUIManager context)
    {
        hintText.gameObject.SetActive(true);
        instructionText.gameObject.SetActive(true);
        
        backPanel.enabled = true;
        
        instructionText.text = instruction;
    }

    public override void UpdateState(HintsUIManager context)
    {
        hintText.text = content;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            context.SwitchState(context.offState);
        }
    }

    public override void ExitState(HintsUIManager context)
    {
        Hide();
    }

    public void SetHintText(string text)
    {
        content = text;
    }

    public override void Hide()
    {
        hintText.gameObject.SetActive(false);
        instructionText.gameObject.SetActive(false);
        backPanel.enabled = false;
    }
}
