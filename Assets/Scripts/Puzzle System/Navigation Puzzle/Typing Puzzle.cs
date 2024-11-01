using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingPuzzle : MonoBehaviour
{
    Queue<string> text;
    Interactable interactable;

    public Text word_output;
    string current_word = string.Empty;
    void Start()
    {
        interactable = GetComponent<Interactable>();
          List<string> wordList = new List<string>
        {
            "apple", "banana", "cherry", "date", "elderberry", "fig", "grape", "honeydew",
            "kiwi", "lemon", "mango", "nectarine", "orange", "papaya", "quince", "raspberry",
            "strawberry", "tangerine", "ugli", "vanilla", "watermelon", "xigua", "yellowfruit", "zucchini"
        };

       text = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable.WasTriggered())
        {   
             Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
    }


}
