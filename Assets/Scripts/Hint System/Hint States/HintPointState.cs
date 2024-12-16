using System.Collections;
using UnityEngine;

public class HintPointState : HintBaseState
{
    private Transform highlightObject;
    public float disableTimer = 10f;
    public override void EnterState(HintsUIManager context)
    {
        context.highlight.enabled = true;   
        context.leftArrow.enabled = false;
        context.rightArrow.enabled = false;
    }

    public override void UpdateState(HintsUIManager context)
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            context.SwitchState(context.offState);
        }

        if (highlightObject)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(highlightObject.position);
            float dot  = Vector3.Dot(Player.Instance.transform.forward, highlightObject.forward);

            if (screenPosition.x < 0)
            {
                context.leftArrow.enabled = true;
                context.rightArrow.enabled = false;
                context.highlight.enabled = false;
                
                Vector3 position = 
                    new Vector3(
                        0,
                        screenPosition.y
                        ); 
                context.leftArrow.transform.position = position;
            }
            else if (screenPosition.x > Screen.width)
            {
                context.rightArrow.enabled = true;
                context.leftArrow.enabled = false;
                context.highlight.enabled = false;

                Vector3 position = 
                    new Vector3(
                        Screen.width,
                        screenPosition.y
                        ); 
                context.rightArrow.transform.position = position;
            }
            else if(dot<=0)
            {
                context.leftArrow.enabled = false;
                context.rightArrow.enabled = false;
                context.highlight.enabled = true;
                context.highlight.transform.position = screenPosition;
            }
            
        }
    }

    public override void ExitState(HintsUIManager context)
    {
        Hide(context);
    }

    public override void Hide(HintsUIManager context)
    {
        context.highlight.enabled = false;    
        context.leftArrow.enabled = false;
        context.rightArrow.enabled = false;
    }
    
    public void SetObject(Transform t)
    {
        highlightObject = t;
    }

    IEnumerator ExitState()
    {
        yield return new WaitForSeconds(disableTimer);
    }
}
