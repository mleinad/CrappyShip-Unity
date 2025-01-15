using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{
    public enum AnimatorType
    {
        Door,
        Terminal
    }

    [System.Serializable]
    public class BoolAudioPair
    {
        public GameObject targetObject; // Object containing the Animator or Script
        public AnimatorType animatorType; // Type of the Animator (Door/Terminal)
        public string boolMemberName; // Name of the bool field, property, or method to monitor
        public AudioSource audioSource; // AudioSource to play
        public bool useAnimator; // True if using Animator, false if monitoring a script
        public UnityEvent onTrue; // Event triggered when the boolean becomes true
        public UnityEvent onFalse; // Event triggered when the boolean becomes false
        [HideInInspector] public bool lastState; // Tracks the previous state to detect changes
    }

    public BoolAudioPair[] boolAudioPairs; // Array of Bool-AudioSource pairs
    public bool debugLogging = false; // Enable logging for debugging

    void Start()
    {
        // Initialize lastState for all pairs
        foreach (var pair in boolAudioPairs)
        {
            pair.lastState = GetBoolValue(pair);
        }
    }

    void Update()
    {
        foreach (var pair in boolAudioPairs)
        {
            if (pair.audioSource == null || pair.targetObject == null) continue;

            bool currentState = GetBoolValue(pair);

            // Trigger audio and events on state change
            if (currentState != pair.lastState)
            {
                if (debugLogging)
                    Debug.Log($"State changed for {pair.boolMemberName} on {pair.targetObject.name}: {currentState}");

                PlayAudio(pair, currentState);

                if (currentState)
                {
                    pair.onTrue.Invoke();
                }
                else
                {
                    pair.onFalse.Invoke();
                }

                pair.lastState = currentState;
            }
        }
    }

    private void PlayAudio(BoolAudioPair pair, bool state)
    {
        // Select specific sound based on animator type
        if (pair.useAnimator)
        {
            switch (pair.animatorType)
            {
                case AnimatorType.Door:
                    pair.audioSource.clip = state ? Resources.Load<AudioClip>("DoorOpen") : Resources.Load<AudioClip>("DoorClose");
                    break;
                case AnimatorType.Terminal:
                    pair.audioSource.clip = state ? Resources.Load<AudioClip>("TerminalActivate") : Resources.Load<AudioClip>("TerminalDeactivate");
                    break;
            }

            if (pair.audioSource.clip != null)
            {
                pair.audioSource.Play();
            }
            else if (debugLogging)
            {
                Debug.LogWarning($"AudioClip missing for {pair.animatorType} on {pair.targetObject.name}");
            }
        }
    }

    private bool GetBoolValue(BoolAudioPair pair)
    {
        if (pair.useAnimator)
        {
            // Handle Animator
            Animator animator = pair.targetObject.GetComponent<Animator>();
            if (animator != null)
            {
                return animator.GetBool(pair.boolMemberName);
            }
        }
        else
        {
            MonoBehaviour[] scripts = pair.targetObject.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                var type = script.GetType();

                // Check for field
                var field = type.GetField(pair.boolMemberName);
                if (field != null && field.FieldType == typeof(bool))
                {
                    return (bool)field.GetValue(script);
                }

                // Check for property
                var property = type.GetProperty(pair.boolMemberName);
                if (property != null && property.PropertyType == typeof(bool))
                {
                    return (bool)property.GetValue(script);
                }

                // Check for method
                var method = type.GetMethod(pair.boolMemberName);
                if (method != null && method.ReturnType == typeof(bool) && method.GetParameters().Length == 0)
                {
                    return (bool)method.Invoke(script, null);
                }
            }
        }

        if (debugLogging)
            Debug.LogWarning($"Bool member {pair.boolMemberName} not found on {pair.targetObject.name}");
        return false;
    }
}