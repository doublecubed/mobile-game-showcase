using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Terminal : MonoBehaviour
{
    public TextMeshProUGUI outputText;
    public TMP_InputField inputField;

    void Start()
    {
        // Add a listener to the input field to detect when the user presses enter
        inputField.onEndEdit.AddListener(ProcessInput);
    }

    void ProcessInput(string input)
    {
        // Handle the input here
        // You can use the input variable to get the user's input
        // You can use the outputText variable to display output to the user
        outputText.text += "> " + input + "\n"; // Display the input in the output text

        // Clear the input field
        inputField.text = "";
        inputField.ActivateInputField();
    }
}
