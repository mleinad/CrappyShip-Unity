using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzInterpreter : BaseInterperter, IPuzzleComponent
{
    private List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }

    // State management
    private bool done = false;  
    private int currentQuestionIndex = 0; 
    private List<string> questions = new List<string> { "How many noodle boxes are in this room?", "How many days have you been on this Ship?","What is the name of the Ship?"};
    private List<string> correctAnswers = new List<string> { "6", "27","CS2075" };
    [SerializeField]
    Interactable interactable;

    private bool isQuizActive = false; 
    
    void Start()
    {
        interactable.enabled = false;
    }

    
    public override List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split();

        if (args.Length > 0)
        {
            if (args[0] == "help")
            {
                response.Add("Solve the quiz to proceed to the next room.");
                response.Add("Type <quiz.exe> to start the quiz.");
                return response;
            }

            if (args[0] == "quiz.exe")
            {
                if (!isQuizActive)
                {
                    StartQuiz();
                }
                else
                {
                    response.Add("The quiz is already running.");
                }
                return response;
            }

            if (isQuizActive)
            {
                CheckAnswer(args[0]);
                return response;
            }
        }

        response.Add("Invalid command. Type <help> for assistance.");
        return response;
    }

    private void StartQuiz()
    {
        isQuizActive = true;
        currentQuestionIndex = 0; 
        response.Add(questions[currentQuestionIndex]);
    }

    // Checks if the provided answer is correct
    private void CheckAnswer(string answer)
    {
        if (answer.ToLower() == correctAnswers[currentQuestionIndex].ToLower()) 
        {
            response.Add("Correct!");

            currentQuestionIndex++;

            if (currentQuestionIndex < questions.Count)
            {
                response.Add(questions[currentQuestionIndex]);
            }
            else
            {
                done = true;
                interactable.enabled = true;
                response.Add("Congratulations! You have completed the quiz.");
                isQuizActive = false; 
            }
        }
        else
        {
            response.Add("Incorrect. Try again.");
        }
    }

    public bool CheckCompletion() => done;

    public void ResetPuzzle()
    {
        done = false;
        currentQuestionIndex = 0;
        interactable.enabled = false;
    }
}

