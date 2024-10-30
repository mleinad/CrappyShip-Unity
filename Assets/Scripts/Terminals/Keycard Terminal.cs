using System.Collections;
using System.Collections.Generic;
using MoodMe;
using UnityEngine;

public class KeycardTerminal : MonoBehaviour, IPuzzleComponent
{
    Interactable interactable;
    public GameObject face_camera;

    bool state = false;

    public float targetTime = 1.0f;
    public float surprised__factor=0.0f;
    IPuzzleComponent keycard;
    void Start()
    {
        interactable = GetComponent<Interactable>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(targetTime == 0)
        {
            state = true;
        }

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
        surprised__factor = EmotionsManager.Emotions.surprised;
        if(surprised__factor>6.0f)
        {
            targetTime -= Time.deltaTime;
        }
        else
        {
            targetTime = 1.0f;
        }

//        Debug.Log(targetTime);
    }

    public bool CheckCompletion() => state;

    public void ResetPuzzle()
    {
        state = false;
    }
}
