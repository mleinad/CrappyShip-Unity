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

        foreach (var content in speech_list)
        {
            Debug.Log("event component -> " + content.GetType());
            if (content.GetTrigger() == component)
            {
            interperter.PushLines(content.GetLines(), 0f);
            StartCoroutine(notification.Appear(content.GetLines()[0], 3));
            content.HandleRepetition(ref speech_list);
            }
        }
    }

}
