using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CodingInterperter : MonoBehaviour, Iinterperter, IPuzzleComponent
{
    Dictionary<string, string> colors = new Dictionary<string, string>{
    {"orange", "#FA4224"},
    {"yellow", "#FDDC5C"},
    {"blue", "#475F94"},
    {"green", "#00ff1b"},
    {"red", "#ff0000"},
    {"white", "#ffffff"}
    };


    public List<string> code_template;
    List<string> response = new List<string>();
    TerminalManager terminalManager;

    [SerializeField]
    Cable cable;

    [SerializeField]
    Interactable interactable;
    bool coding = false;

    bool locked = true;

    bool state;
    void Start()
    {
        terminalManager = GetComponent<TerminalManager>();
        code_template = new List<string>();
        state = false;
        interactable.enabled = false;
    }

    public List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split();

        if(locked)
        {
            if(args[0] == "help")
            {
            ListEntry("ove", "returns a list of commands");
            ListEntry("override", "pauses the game");

            return response;
        
            }
            if(args[0]=="password")
            {
                if(args[1]=="1234password")
                {
                    response.Add("correct password, developer mode on");
                    locked = false;
                    return response;
                }
                else
                {
                    response.Add("incorrect password");
                    return response;
                }
            }
            else
            {
                response.Add("Command not recognized. Type help for a list of commands");
                return response;
            }
        }
        else
        {
            if(args[0]=="code")
            {
                Code();
                return response;

            }
            if(args[0]=="ascii")
            {
                LoadTitle("ASCII.txt","blue", 2);
                return response;
            }
            if(args[0] == "help")
            {
                ListEntry("help", "returns a list of commands");
                ListEntry("stop", "pauses the game");
                ListEntry("run", "resumes the game");
                ListEntry("four", "bla bla bla");
                return response;
            
            }
            if(args[0]=="boop")
            {
                response.Add("Thank you for using the terminal");
                response.Add("---------------------------------");

                return response;
            }
            else
            {
                response.Add("Command not recognized. Type help for a list of commands");
                return response;
            }

        }
    }


    #region style
    public string BoldString(string s)
    {
        return "<b>" + s + "</b>";
    }
    
    public string HighlightString(string s, string color)
    {
        string leftTag = "<mark=" + color + ">";
        string rightTag = "</mark>";
        
        return leftTag + s + rightTag;
    }

    public string ColorString(string s, string color)
    {
        string leftTag = "<color=" + color + ">";
        string rightTag ="</color>";

        return leftTag + s + rightTag;
    }

    #endregion
    void ListEntry(string a, string b)
    {
        response.Add(ColorString(a, colors["orange"]) + ":" + ColorString(b, colors["yellow"]));
    }

    void LoadTitle(string path, string color, int spacing)
    {
        StreamReader file = new StreamReader(Path.Combine(Application.streamingAssetsPath, path));
        for(int i =0; i< spacing; i++)
        {
            response.Add("");
        }
        while(!file.EndOfStream)
        {   
            string temp_line = file.ReadLine();
            code_template.Add(temp_line);    
            if(color == string.Empty) 
            {
                response.Add(temp_line);
            }
            else 
            {
                response.Add(ColorString(temp_line, colors[color]));
            }
        }
        for(int i=0; i< spacing; i++)
        {
            response.Add("");
        }
        file.Close();
    }


#region Typing Game

   int lineIndex = 0;
   public string currentString;
   public string remainingString;


    void Code()
    {
        LoadTitle("Code.txt", "white", 0);
        coding = true;

        remainingString = code_template[lineIndex];
        terminalManager.UserInputState(false);

        StartCoroutine(Timer());
    }

 
    public void Update()
    {
        if(cable.GetSignal()>0)
        {
            interactable.enabled = true;
        }
        else interactable.enabled = false;


        if(coding)
        {   
            if(terminalManager.GetDynamicLines().Count>0)
            {
                
                if(remainingString == string.Empty)
                {
                    if(Input.GetKeyDown(KeyCode.Return))
                    {
                        lineIndex++;
                        remainingString = code_template[lineIndex];
                    }
                }
                
                CheckInput();

            
            }
        }
    }

    private void CheckInput()
    {
        if(Input.anyKeyDown)
        {
            string input = Input.inputString;
            if(input.Length==1)
            {
                EnterLetter(input);
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
       return remainingString.IndexOf(letter) == 0;
    }

    private void EnterLetter(string letter)
    {
        char expectedLetter = remainingString[0];

        if(IsCorrectLetter(letter))
        {   
    
            currentString += HighlightString(letter, "blue");
             RemoveLetter();
            terminalManager.GetDynamicLines()[lineIndex].text = currentString + remainingString;

            if(isLineComplete())
            {
                lineIndex ++;   //moves to next line
                if (lineIndex < code_template.Count)
                {
                    // Set up for the next line
                    //SetRemainingWord(terminalManager.dynamic_lines[lineIndex].text);
                    currentString = ""; // Reset `currentString` for the new line
                    remainingString = code_template[lineIndex];
                }
                else
                {
                    coding = false; // Stop coding if all lines are complete
                }
            }
        }
        else
        {   
            terminalManager.GetDynamicLines()[lineIndex].text =  currentString+ HighlightString(expectedLetter.ToString(), "red") + remainingString;
        }
    }
    private void RemoveLetter()
    {
        string new_string = remainingString.Remove(0,1);
        SetRemainingWord(new_string);
    }

    private void SetRemainingWord(string line)
    {
        remainingString = line;
    }

    private bool isLineComplete()
    {
        return remainingString.Length == 0;
    }

    public bool CheckCompletion()
    {
        return state;
    }

    public void ResetPuzzle()
    {
        state=false;
    }
    #endregion

    IEnumerator Timer()
    {

        yield return new  WaitForSeconds(8);

        foreach(var line in terminalManager.GetDynamicLines())
        {
            line.text = ColorString("!!!!!!!!____I WON____!!!!!!!!!!!", "red");
        }
    
    }
}
