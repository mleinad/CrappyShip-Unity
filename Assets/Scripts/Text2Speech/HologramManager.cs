using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramManager : MonoBehaviour
{
    [SerializeField] private GameObject hologramObject;
    [SerializeField] private Animator hologramAnimator;
    [SerializeField] private string animationTriggerTextToSpeech = "Talk";
    [SerializeField] private string animationTriggerPuzzleComplete = "Completed";


    [SerializeField] private IPuzzleComponent puzzleComponent;


    private bool isPuzzleComplete = false;

    void Start()
    {
        hologramObject.SetActive(true);   
    }

    void Update()
    {

        if (puzzleComponent.CheckCompletion() && !isPuzzleComplete)
        {
            HandlePuzzleCompletion();
        }
    }

    // Method to handle the TTS activation event
    public void OnTextToSpeechActivated()
    {
        hologramObject.SetActive(true);

        if (hologramAnimator != null)
        {
            hologramAnimator.SetTrigger(animationTriggerTextToSpeech); 
        }
    }

    // Method to handle when the puzzle is completed
    private void HandlePuzzleCompletion()
    {
        isPuzzleComplete = true;


        if (hologramAnimator != null)
        {
            hologramAnimator.SetTrigger(animationTriggerPuzzleComplete);
        }

    }
}
