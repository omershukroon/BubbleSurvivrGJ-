using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Import TextMeshPro namespace

public class LevelsMenuBehaviour : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component for displaying total stars
    public TextMeshProUGUI totalStarsText;

    // Method to handle button click
    public void OnLevelClick(GameObject clickedObject)
    {
        Debug.Log("Button clicked!!");

        // Ensure the clickedObject is not null
        if (clickedObject == null)
        {
            Debug.LogError("clickedObject is null! Make sure the button is correctly linked.");
            return;
        }

        // Print the name of the clicked object to ensure it's the correct one
        Debug.Log("Clicked object name: " + clickedObject.name);

        // Check if the clicked object has the "LevelNumberText" child
        Transform levelNumberTextTransform = clickedObject.transform.Find("LevelNumberText");

        // Check if the child is found
        if (levelNumberTextTransform == null)
        {
            Debug.LogError("No child named 'LevelNumberText' found in this GameObject: " + clickedObject.name);
            return;
        }

        Debug.Log("Found 'LevelNumberText' child!");

        // Get the TextMeshProUGUI component from the "LevelNumberText" child
        TextMeshProUGUI childText = levelNumberTextTransform.GetComponent<TextMeshProUGUI>();
        if (childText == null)
        {
            Debug.LogError("No TextMeshProUGUI component found in 'LevelNumberText' child.");
            return;
        }

        Debug.Log("Found 'TextMeshProUGUI' component inside 'LevelNumberText'!");

        // Try to parse the level number from the text
        if (int.TryParse(childText.text, out int levelNumber))
        {
            Debug.Log("Level number parsed: " + levelNumber);

            // Load the scene based on the level number (levelNumber + 1 to match scene index)
            LoadSceneByIndex(levelNumber);
        }
        else
        {
            Debug.LogError("Text in 'LevelNumberText' is not a valid number. Text was: " + childText.text);
        }
    }

    // Method to load a scene based on its index
    void LoadSceneByIndex(int levelNumber)
    {
        Debug.Log("Attempting to load scene for level: " + levelNumber);

        // Here, we try to load the scene based on the level number
        if (levelNumber >= 0 && levelNumber < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Loading scene with index: " + levelNumber);
            SceneManager.LoadScene(levelNumber + 1);  // Use levelNumber directly (assumes scenes are in build settings)
        }
        else
        {
            Debug.LogError("Scene index " + levelNumber + " is not valid. Check your Build Settings.");
        }
    }

    // Update the total stars text when the scene loads
    void Start()
    {
        UpdateTotalStars();
    }

    // Method to update the total stars text
    void UpdateTotalStars()
    {
        if (PersistentObjectManager.Instance != null)
        {
            int totalStars = PersistentObjectManager.Instance.GetSumStarRating();
            if (totalStarsText != null)
            {
                totalStarsText.text = "" + totalStars;
            }
        }
    }
}
