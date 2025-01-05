using UnityEngine;

public class HologramManager : MonoBehaviour
{
    [SerializeField] private GameObject hologramObject;
    [SerializeField] private Animator hologramAnimator;

    private bool isPuzzleComplete = false;

    public static HologramManager Instance;
    void Start()
    {
        Instance = this;
        hologramObject.SetActive(false);
        EventManager.Instance.onAiTrigger += OnTextToSpeechActivated;
    }

    void Update()
    {
                    //  OnTextToSpeechActivated();
    }

    public void OnTextToSpeechActivated(IPuzzleComponent puzzleComponent)
    {
        hologramObject.SetActive(true);

        if (hologramAnimator != null)
        {
            hologramAnimator.SetTrigger("Talk"); 
        }
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
