using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject GUI;
    public bool IsPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        GUI.SetActive(IsPaused);
        
        if (IsPaused)
        {
            Time.timeScale = 0f;
            
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
