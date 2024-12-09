using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class BoolAudioPair
    {
        public GameObject targetObject; // Object containing the Animator or Script
        public string boolMemberName; // Name of the bool field, property, or method to monitor
        public AudioSource audioSource; // AudioSource to play
        public bool useAnimator; // True if using Animator, false if monitoring a script
    }

    public BoolAudioPair[] boolAudioPairs; // Array of Bool-AudioSource pairs

    void Update()
    {
        foreach (var pair in boolAudioPairs)
        {
            if (pair.audioSource == null || pair.targetObject == null) continue;

            bool isBoolTrue = false;

            if (pair.useAnimator)
            {
                // Handle Animator
                Animator animator = pair.targetObject.GetComponent<Animator>();
                if (animator != null)
                {
                    isBoolTrue = animator.GetBool(pair.boolMemberName);
                }
            }
            else
            {
                // Handle Custom Scripts
                MonoBehaviour[] scripts = pair.targetObject.GetComponents<MonoBehaviour>();
                foreach (var script in scripts)
                {
                    // Use Reflection to check for the field, property, or method
                    var type = script.GetType();

                    // Check for field
                    var field = type.GetField(pair.boolMemberName);
                    if (field != null && field.FieldType == typeof(bool))
                    {
                        isBoolTrue = (bool)field.GetValue(script);
                        break;
                    }

                    // Check for property
                    var property = type.GetProperty(pair.boolMemberName);
                    if (property != null && property.PropertyType == typeof(bool))
                    {
                        isBoolTrue = (bool)property.GetValue(script);
                        break;
                    }

                    // Check for method
                    var method = type.GetMethod(pair.boolMemberName);
                    if (method != null && method.ReturnType == typeof(bool) && method.GetParameters().Length == 0)
                    {
                        isBoolTrue = (bool)method.Invoke(script, null);
                        break;
                    }
                }
            }

            // Manage AudioSource based on bool value
            if (isBoolTrue && !pair.audioSource.isPlaying)
            {
                pair.audioSource.Play();
            }
            else if (!isBoolTrue && pair.audioSource.isPlaying)
            {
                pair.audioSource.Stop();
            }
        }
    }
}
