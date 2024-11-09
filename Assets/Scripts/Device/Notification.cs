using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public float startY = -500f;          
    public float endY = 19f;               
    public float duration = 0.5f;         

    // Bezier-inspired easing to mimic a smooth curve for popup motion
    public Ease bezierEase = Ease.OutBounce;

    private Vector3 originalPosition;

    public bool play;

    [SerializeField]
    TMP_Text text;

    RectTransform  rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.position;
        play = false;
    }

    void Update()
    {
        if(play) Appear();
    }
    void Appear()
    {
        
    }

    void Disappear()
    {

    }

      public void ResetPosition()
    {
        transform.localPosition = originalPosition;
    }
}
