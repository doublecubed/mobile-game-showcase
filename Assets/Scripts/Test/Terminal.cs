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
        
        // Handle the input here
        // You can use the input variable to get the user's input
        // You can use the outputText variable to display output to the user
        outputText.text += "> " + input + "\n"; // Display the input in the output text

        // Clear the input field
        inputField.text = "";
        inputField.ActivateInputField();
    }
}
