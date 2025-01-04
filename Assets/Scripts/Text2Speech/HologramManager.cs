using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramManager : MonoBehaviour
{
    [SerializeField] private GameObject hologramObject;
    [SerializeField] private Animator hologramAnimator;

    private bool isPuzzleComplete = false;

    void Start()
    {
        hologramObject.SetActive(false);   
    }

    void Update()
    {
            OnTextToSpeechActivated();
    }

    public void OnTextToSpeechActivated()
    {
        hologramObject.SetActive(true);

        if (hologramAnimator != null)
        {
            hologramAnimator.SetTrigger("Talk"); 
        }
    }

    private void HandlePuzzleCompletion()
    {
        isPuzzleComplete = true;

        hologramObject.SetActive(true);
        if (hologramAnimator != null)
        {
            
            hologramAnimator.SetTrigger("TaskCompleted");
        }

        isPuzzleComplete = false;
    }
}
