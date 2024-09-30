using UnityEngine;
using UnityEngine.UI;

public class ButtonTest1 : MonoBehaviour
{
    public Button spearButton; // Reference to the Button
    private int spearLevel = 0;
    private int maxSpearLevel = 3;

    void Start()
    {
        // Initialize the button text
        spearButton.GetComponentInChildren<Text>().text = "Spear (0/3)";

        // Add listener for button click
        spearButton.onClick.AddListener(UpgradeSpear);
    }

    void UpgradeSpear()
    {
        if (spearLevel < maxSpearLevel)
        {
            spearLevel++;
            spearButton.GetComponentInChildren<Text>().text = $"Spear ({spearLevel}/{maxSpearLevel})";
        }
    }
}
