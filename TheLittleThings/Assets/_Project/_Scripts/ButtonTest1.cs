using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class SpearUpgrade : MonoBehaviour
{
    public Button spearButton; // Assign this in the Unity Editor
    public Text spearButtonText; // The text on the button to show the progress
    private int currentLevel = 0; // Start at 0 upgrades
    private int maxLevel = 3; // Max upgrades is 3

    void Start()
    {
        // Initialize the button's text
        UpdateSpearText();

        // Add a listener to the button to handle clicks
        spearButton.onClick.AddListener(OnSpearButtonClick);

        // Debugging 
        if (spearButton == null)
        {
            Debug.LogError("Spear Button is not assigned");
        }
        if (spearButtonText == null)
        {
            Debug.LogError("Spear Button Text is not assigned");
        }
    }

    void OnSpearButtonClick()
    {
        // Increment the level if it's not at max level yet
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            UpdateSpearText();
        }
    }

    void UpdateSpearText()
    {
         if (spearButtonText != null)
        {
        // Update the button text to show the current progress (e.g., 1/3)
        spearButtonText.text = "Spear (" + currentLevel + "/" + maxLevel + ")";
        
        // Disable button when max level is reached
            if (currentLevel >= maxLevel)
            {
                spearButton.interactable = false; // Disable the button
                spearButtonText.text = "Spear (MAX)";
            }
        }
        else
        {
            Debug.LogError("TMP Text component is missing!");
        }
    }
}