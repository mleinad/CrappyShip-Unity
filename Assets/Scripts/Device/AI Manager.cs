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

    [SerializeField] private Text2Speech textToSpeech;

    void Start()
    {
             EventManager.Instance.onAiTrigger += Perform;
    }
    public string CleanTextForPolly(string inputText)
    {
        // Remove any <color> or other HTML-style tags
        string cleanText = System.Text.RegularExpressions.Regex.Replace(inputText, "<.*?>", string.Empty);
        string cleanedText = cleanText.Replace("AI->", "").Trim();
        return cleanedText;
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
                    if (textToSpeech != null)
                    {
                        // Send the first line of the dialog to Text2Speech to convert to speech
                        string cleanedText = CleanTextForPolly(content.GetLines()[0]);
                        textToSpeech.ConvertTextToSpeech(cleanedText);
                    }
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
