using System.Collections;
using UnityEngine;

public class HologramManager : MonoBehaviour
{
     public GameObject hologramObject;
     private Animator hologramAnimator;

     public bool docked = false;
    private bool isPuzzleComplete = false;

    public static HologramManager Instance;

    private Transform parent;
    private Vector3 lPosition;
    private Vector3 lRotation;
    void Start()
    {
        Instance = this;
        hologramObject.SetActive(false);
        EventManager.Instance.onAiTrigger += OnTextToSpeechActivated;
        
        parent = hologramObject.transform.parent;
        lPosition = hologramObject.transform.localPosition;
        lRotation = hologramObject.transform.localEulerAngles;
    }

    void Update()
    {
                    //  OnTextToSpeechActivated();
    }

    public void OnTextToSpeechActivated(IPuzzleComponent puzzleComponent)
    {
        if(!docked)
            StartCoroutine(PopUp());

        if (hologramAnimator != null)
        {
            hologramAnimator.SetTrigger("Talk"); 
        }


    }

    public IEnumerator PopUp()
    {
        hologramObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        PopDown();
    }

    public void PopDown()
    {
        hologramObject.SetActive(false);
    }

    public void ResetPosition()
    {
        hologramObject.transform.SetParent(parent, false); 
        hologramObject.transform.localPosition = lPosition;
        hologramObject.transform.localEulerAngles = lRotation;
    }
    public void HandlePuzzleCompletion()
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
