using System;
using System.Collections.Generic;
using QFSW.QC.Utilities;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour
{
    public GameObject directory_line;
    public GameObject response_line;
    public TMP_InputField terminal_input;
    public GameObject user_input_line;
    public ScrollRect sr;
    public GameObject msgList;
    Iinterperter interperter;


    //teste
   // public List<>

    private void Start()
    {
        interperter = GetComponent<Iinterperter>();    
    }

    private void OnGUI()
    {
        if(terminal_input.isFocused && terminal_input.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            //store whatever the user typed
            string user_input = terminal_input.text; 

            //clear the input field
            ClearInputField();

            //Instanciate GameObject with a directory prefix
            AddDirectoryLine(user_input);


            int lines = AddInterperterLines(interperter.Interpert(user_input));

            ScrollToBottom(lines);

            user_input_line.transform.SetAsLastSibling();
            terminal_input.ActivateInputField();
            terminal_input.Select();
        }
    }

    private void AddDirectoryLine(string user_input)
    {
        //resize the command line container -> weird stuff on scroll rect other wise
       Vector2 msgListSize = msgList.GetComponent<RectTransform>().sizeDelta;
       msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(msgListSize.x, msgListSize.y+ 35.0f);

       //instantiate the directory line
       GameObject msg = Instantiate(directory_line, msgList.transform);

       msg.transform.SetSiblingIndex(msgList.transform.childCount -1);

       msg.GetComponentsInChildren<TMP_Text>()[1].text = user_input;

    }

    private void ClearInputField()
    {
        terminal_input.text ="";
    }



    int AddInterperterLines(List<string> interpertation)
    {
        for(int i=0; i<interpertation.Count; i++)
        {
            GameObject res = Instantiate(response_line, msgList.transform);

            res.transform.SetAsLastSibling();

            Vector2 list_size = msgList.GetComponent<RectTransform>().sizeDelta;
            msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(list_size.x, list_size.y + 35.0f);
            res.GetComponentInChildren<TMP_Text>().text = interpertation[i];
        }

        return interpertation.Count;
    }


    void ScrollToBottom(int lines)
    {
        if(lines > 4)
        {
            sr.velocity = new Vector2(0, 450);
        }else
        {
            sr.verticalNormalizedPosition = 0;
        }
    }

    public void UserInputState(bool state)
    {
        user_input_line.gameObject.SetActive(state);
    }


}
