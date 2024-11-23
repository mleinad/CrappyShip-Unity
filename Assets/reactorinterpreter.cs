using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reactorinterpreter : BaseInterperter
{
    public TerminalManager terminalManager;
    private List<string> _response = new List<string>();

    public override List<string> response
    {
        get { return _response; }
        set { _response = value; }
    }

    private int energyLevel = 50; // Initial energy level (you can adjust this)
    private const int maxEnergy = 100;
    private const int minEnergy = 0;

    private void Update()
    {
        // Listen for arrow key inputs to adjust the energy level
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AdjustEnergy(5);  // Increase energy by 5
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AdjustEnergy(-5);  // Decrease energy by 5
        }
    }
    public override List<string> Interpert(string input)
    {
        List<string> response = new List<string>();

        // Check for commands entered in the terminal (e.g., "increase energy", "decrease energy", etc.)
        string[] args = input.Split();

        if (args.Length > 1)
        {
            switch (args[0].ToLower())
            {
                case "increase":
                    if (args[1].ToLower() == "energy")
                    {
                        AdjustEnergy(5);  // Increase energy by 5
                        response.Add("Energy increased by 5.");
                    }
                    break;

                case "decrease":
                    if (args[1].ToLower() == "energy")
                    {
                        AdjustEnergy(-5);  // Decrease energy by 5
                        response.Add("Energy decreased by 5.");
                    }
                    break;

                case "set":
                    if (args[1].ToLower() == "energy")
                    {
                        if (args.Length > 2 && int.TryParse(args[2], out int targetValue))
                        {
                            SetEnergy(targetValue);
                            response.Add($"Energy set to {targetValue}.");
                        }
                        else
                        {
                            response.Add("Invalid energy value.");
                        }
                    }
                    break;

                default:
                    response.Add("Unknown command.");
                    break;
            }
        }
        else
        {
            response.Add("Please provide a command.");
        }

        return response;
    }

    private void SetEnergy(int value)
    {
        // Set the energy to a specific value (within bounds)
        energyLevel = Mathf.Clamp(value, minEnergy, maxEnergy);
        DisplayEnergy();  // Update the terminal with the new energy level and ASCII progress bar
    }

    private void DisplayEnergy()
    {
        // Calculate the number of filled blocks based on the energy level
        int filledBlocks = energyLevel / 10; // Divide by 10 for a scale of 0-10

        // Create the ASCII art bar
        string energyBar = "[";
        for (int i = 0; i < 10; i++)
        {
            if (i < filledBlocks)
            {
                energyBar += "?"; // Filled part
            }
            else
            {
                energyBar += " "; // Empty part
            }
        }
        energyBar += "]";

        // Display the energy bar
        terminalManager.NoUserInputLines(new List<string> { $"Energy Level: {energyLevel}% {energyBar}" });
    }

    private void AdjustEnergy(int adjustment)
    {
        // Adjust the energy level and clamp it between 0 and 100
        energyLevel += adjustment;
        energyLevel = Mathf.Clamp(energyLevel, minEnergy, maxEnergy);

        // Display the updated energy level
        DisplayEnergy();
    }
}
