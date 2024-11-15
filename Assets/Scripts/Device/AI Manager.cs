using System.Collections;
using System.Collections.Generic;
using Mono.CSharp;
using UnityEditor.Rendering;
using UnityEngine;

public class AIManager : MonoBehaviour
{

    Dictionary <IPuzzleComponent, DialogContent> speech_dictionary; //might need to create costume speech bouble object 
    public List<GameObject> gOlist;
    public AI_Interperter interperter;
    public Notification notification;

    void Start()
    {
        EventManager.Instance.onAiInteraction += Perform;

            speech_dictionary = new Dictionary<IPuzzleComponent, DialogContent>();

            foreach(GameObject @object in gOlist)
            {
                IPuzzleComponent key = @object.GetComponent<IPuzzleComponent>();
                DialogContent content = @object.GetComponent<DialogContent>();
                if(key!=null && content!=null)
                {
                    speech_dictionary.Add(key, content);
//                      Debug.Log(key);
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        foreach(var pair in speech_dictionary)
        {
            if(pair.Key.CheckCompletion())
            {
                interperter.PushLines(pair.Value, 0f);
                
                speech_dictionary.Remove(pair.Key); //if only meant to happen ones 

                notification.Appear(pair.Value[0], 3);
            }
        }
        */
    }


    void Perform(IPuzzleComponent component)
    {
        Debug.Log(component);
        if(speech_dictionary==null) return;

        if(speech_dictionary.ContainsKey(component))
        {
            interperter.PushLines(speech_dictionary[component].GetLines(), 0f);

            StartCoroutine(notification.Appear(speech_dictionary[component].GetLines()[0],3));
        
            speech_dictionary.Remove(component);
        }
        else Debug.Log("doesnt contain key ");
    }

}
