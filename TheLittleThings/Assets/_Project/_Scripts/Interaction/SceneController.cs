using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class SceneController : MonoBehaviour
{
    public String sceneName;
    void Update() {
        if (Interacting.interacted) {
            NextScene();
        }
    }
    public void NextScene () {
        SceneTransitionManager.Instance.LoadScene(sceneName);
    }


}
