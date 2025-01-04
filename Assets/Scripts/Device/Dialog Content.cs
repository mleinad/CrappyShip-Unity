using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogContent : MonoBehaviour
{
    public List<string> dialog;
    public AudioClip audioLine;
    [SerializeField] private AudioSource playerAudioSource;

    public bool isRepeated = false;
    int repCount;
    private int _count;

    [SerializeField] private IPuzzleComponent trigger;
    [SerializeField] [CanBeNull] private DialogContent previousDialog;
    private void Awake()
    {
        trigger = GetComponent<IPuzzleComponent>();
        repCount = 0;
    }
    
    public IPuzzleComponent GetTrigger() => trigger;
    
    public bool CanPlay()
    {
        if (previousDialog != null)
        {
            return previousDialog.trigger.CheckCompletion();
        }
        return true;
    }
    public List<string> GetLines()
    {
        //if been repeated could change...

        return dialog;
    }
    public IEnumerator PlayAudioAtPlayerPosition()
    {
        if (audioLine != null && playerAudioSource != null)
        {
            // Stop any currently playing audio
            playerAudioSource.Stop();

            // Play the new audio clip from the player's AudioSource
            playerAudioSource.clip = audioLine;
            playerAudioSource.Play();

            // Wait until the audio has finished playing
            while (playerAudioSource.isPlaying)
            {
                yield return null; // Wait until the audio finishes playing
            }
        }
        else
        {
            Debug.LogWarning("Audio clip or player AudioSource is missing.");
        }
    }

    public void HandleRepetition(ref List<DialogContent> list)
    {
        if(!isRepeated) list.Add(this);
        else repCount++;
    }
}
