using System.Collections;
using System.Collections.Generic;
using MoodMe;
using UnityEngine;

public class KeycardTerminal : MonoBehaviour
{
    Interactable interactable;
    public GameObject face_camera;


    void Start()
    {
        interactable = GetComponent<Interactable>();

    }

    // Update is called once per frame
    void Update()
    {
        if(interactable.WasTriggered())
        {
           face_camera.SetActive(true); 
           EmotionHandling(); 
        }
        else
        {
            face_camera.SetActive(false);
        }
    }

    void EmotionHandling()
    {
     //   Debug.Log(EmotionsManager.Emotions.angry);

    }
}
