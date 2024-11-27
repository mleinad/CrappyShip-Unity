using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzInterpreter : BaseInterperter
{
    private List<string> _response = new List<string>();
    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }

    // State management
    private int currentQuestionIndex = 0;  // Tracks which question the user is answering
    private List<string> questions = new List<string> { "How many noodle boxes are in this room?", "What is the color of the sky?" };
    private List<string> correctAnswers = new List<string> { "6", "blue" };  // Answers corresponding to questions

    private bool isQuizActive = false;  // Determines if the quiz is currently active

    public override List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split();  // Split the input to handle commands or answers

        if (args.Length > 0)
        {
            // Command to get help
            if (args[0] == "help")
            {
                response.Add("Solve the quiz to proceed to the next room.");
                response.Add("Type <quizz.exe> to start the quiz.");
                return response;
            }

            // Command to start the quiz
            if (args[0] == "quizz.exe")
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

            // If quiz is running, check for answers
            if (isQuizActive)
            {
                CheckAnswer(args[0]);
                return response;
            }
        }

        // If the input is not recognized
        response.Add("Invalid command. Type <help> for assistance.");
        return response;
    }

    // Starts the quiz and displays the first question
    private void StartQuiz()
    {
        isQuizActive = true;
        currentQuestionIndex = 0;  // Start at the first question
        response.Add(questions[currentQuestionIndex]);  // Display the first question
    }

    // Checks if the provided answer is correct
    private void CheckAnswer(string answer)
    {
        if (answer.ToLower() == correctAnswers[currentQuestionIndex].ToLower())  // Answer check is case-insensitive
        {
            response.Add("Correct!");

            // Move to the next question
            currentQuestionIndex++;

            if (currentQuestionIndex < questions.Count)
            {
                response.Add(questions[currentQuestionIndex]);  // Display next question
            }
            else
            {
                response.Add("Congratulations! You have completed the quiz.");
                isQuizActive = false;  // End the quiz
            }
        }
        else
        {
            response.Add("Incorrect. Try again.");
        }
    }
}

