using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public float startY = -100f;          
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
    public IEnumerator Appear(string msg, float time)
    {
        text.text = msg;
        rectTransform.DOAnchorPosY(endY, duration);
        yield return new WaitForSeconds(time);
        Disappear();
    }

    private void Disappear()
    {
        rectTransform.DOAnchorPosY(startY, duration);
        text.text = string.Empty;
    }

      public void ResetPosition()
    {
        transform.localPosition = originalPosition;
    }
}
