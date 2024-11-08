using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TextBoxScaler : MonoBehaviour
{
    
    private RectTransform canvasRect;
    [SerializeField]
    private RectTransform textBoxRect;
   
    [SerializeField]
    private RectTransform inputText;
    
    void Start()
    {
        //textBoxRect = transform.GetChild(1).GetComponent<RectTransform>();    set manually

        Canvas parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas != null)
        {
            canvasRect = parentCanvas.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("No Canvas found in parent hierarchy!");
            return;
        }

        float rightEdgePosition = canvasRect.rect.width;
        float textBoxLeftPosition = textBoxRect.anchoredPosition.x;

        float newWidth = rightEdgePosition - textBoxLeftPosition;

        textBoxRect.sizeDelta = new Vector2(newWidth, textBoxRect.sizeDelta.y);

        if(inputText!=null)
        {
             inputText.sizeDelta = new Vector2(newWidth, inputText.sizeDelta.y);
        }
    }

}
