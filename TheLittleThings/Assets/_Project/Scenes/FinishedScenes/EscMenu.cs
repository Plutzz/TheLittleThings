using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Load the "Menu" scene
            SceneManager.LoadScene("Menu");
        }
    }
}
