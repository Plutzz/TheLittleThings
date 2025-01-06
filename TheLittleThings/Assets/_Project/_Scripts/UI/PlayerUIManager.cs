using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUIManager : Singleton<PlayerUIManager>
{
    [SerializeField] private GameObject inGameUI, resultsUI;
    [SerializeField] private TextMeshProUGUI resultsTimerText;
    [SerializeField] private float returnTime;
    private bool resultsTimerActive;
    private float resultsTimer;

    private void Update()
    {
        if (resultsTimerActive)
        {
            HandleResultsTimer();
        }
    }
    public void SetInGameUI(bool active)
    {
        inGameUI.SetActive(active);
    }
    
    public void SetResultsUI(bool active)
    {
        resultsUI.SetActive(active);
        resultsTimerActive = active;
        if (active)
        {
            resultsTimer = returnTime;
        }
    }

    private void HandleResultsTimer()
    {
        resultsTimer -= Time.deltaTime;
        resultsTimerText.text = " RETURNING TO TOWN IN: " + resultsTimer.ToString("0") + " SECONDS";
        if (resultsTimer <= 0f)
        {
            SceneManager.LoadScene("Village Scene");
            resultsTimerActive = false;
        }
    }
    

}
