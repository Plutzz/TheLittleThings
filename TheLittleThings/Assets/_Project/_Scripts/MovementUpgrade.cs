using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovementUpgrade : MonoBehaviour
{
    public Button movementButton; 
    public TextMeshProUGUI movementButtonText; 
    [SerializeField] private int maxLevel = 1; 
    private int currentLevel = 0;

    void Start()
    {
        UpdateButtonText();

        movementButton.onClick.AddListener(OnMovementButtonClick);
    }

    public void UnlockMovementButton()
    {
        movementButton.interactable = true;
    }

    void OnMovementButtonClick()
    {
=        if (currentLevel < maxLevel)
        {
            currentLevel++;
            UpdateButtonText();

            if (currentLevel >= maxLevel)
            {
                movementButton.interactable = false;
                movementButtonText.text = "Movement (MAX)";
            }
        }
    }

    void UpdateButtonText()
    {
        if (movementButtonText != null)
        {
            movementButtonText.text = "Movement (" + currentLevel + "/" + maxLevel + ")";
        }
        else
        {
            Debug.LogError("Text component for Movement is missing!");
        }
    }
}
