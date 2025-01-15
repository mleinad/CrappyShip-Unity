using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class AIManager : MonoBehaviour
{

    public List<DialogContent> speech_list;
    public AI_Interperter interperter;
    public Notification notification;
    private bool isAudioPlaying = false;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
             EventManager.Instance.onAiTrigger += Perform;
    }
    private async Task WaitForAudioToFinish()
    {
        while (audioSource.isPlaying)
        {
            await Task.Yield();
        }

        isAudioPlaying = false;
    }

    private async void Perform(IPuzzleComponent component)
    {
        if(speech_list==null) return;

        List<DialogContent> itemsToRemove = new List<DialogContent>();

        if (isAudioPlaying)
        {
            await WaitForAudioToFinish();
        }


        foreach (var content in speech_list)
        {
            if (content.GetTrigger() == component)
            {
                if (content.CanPlay() && content.GetTrigger().CheckCompletion())
                {
                    isAudioPlaying = true;
                    interperter.PushLines(content.GetLines(), 0f);
                 //   StartCoroutine(notification.Appear(content.GetLines()[0], 3));
                    StartCoroutine(content.PlayAudioAtPlayerPosition());
                    await WaitForAudioToFinish();
                    content.HandleRepetition(ref itemsToRemove);
                }
            }
        }

        foreach (var item in itemsToRemove)
        {
            speech_list.Remove(item);
        }
    }

}
