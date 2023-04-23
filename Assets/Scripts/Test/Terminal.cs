using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using IslandGame.PuzzleEngine;

public class Terminal : MonoBehaviour
{
    public TextMeshProUGUI outputText;
    public TMP_InputField inputField;

    public PuzzleSolver solver;
    
    void Start()
    {
        // Add a listener to the input field to detect when the user presses enter
        inputField.onEndEdit.AddListener(ProcessInput);
        
        DisplayPuzzleState();
    }

    void ProcessInput(string input)
    {
        if (input == "undo")
        {
            solver.UndoLastCommand();
            return;
        }
        
        string first = input[0].ToString();
        int firstInt = Convert.ToInt32(first);
        
        string second = input[2].ToString();
        int secondInt = Convert.ToInt32(second);
        
        solver.RegisterCommand(firstInt, secondInt);

        DisplayPuzzleState();

        // Clear the input field
        inputField.text = "";
        inputField.ActivateInputField();
    }

    private void DisplayPuzzleState()
    {
        string output = "Puzzle State:\n";
        int[,] state = solver.GetPuzzleState();
        for (int i = 0; i < state.GetLength(0); i++)
        {
            output += "Line " + i + ": ";
            
            for (int j = 0; j < state.GetLength(1); j++)
            {
                output += state[i, j] + ".";
            }

            output += "\n";
        }
        
        outputText.text = output; // Display the input in the output text
    }
}
