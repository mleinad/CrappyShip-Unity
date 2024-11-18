using System.Collections;
using System.Collections.Generic;
using Mono.CSharp;
using UnityEditor.Rendering;
using UnityEngine;

public class AIManager : MonoBehaviour
{

    public List<DialogContent> speech_list;
    public AI_Interperter interperter;
    public Notification notification;

    void Start()
    {
             EventManager.Instance.onAiInteraction += Perform;
    }

    // Update is called once per frame

    void Perform(IPuzzleComponent component)
    {
        if(speech_list==null) return;

        List<DialogContent> itemsToRemove = new List<DialogContent>();
        
        foreach (var content in speech_list)
        {
            if (content.GetTrigger() == component)
            {
                if (content.CanPlay() && content.GetTrigger().CheckCompletion())
                {
                    interperter.PushLines(content.GetLines(), 0f);
                    StartCoroutine(notification.Appear(content.GetLines()[0], 3));
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
