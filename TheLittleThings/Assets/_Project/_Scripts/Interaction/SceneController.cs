using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public String sceneName;
    [SerializeField] Animator fadeTransition;

    // Update is called once per frame
    public void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }

    void Update() {
        if (Interacting.interacted) {
            NextScene();
        }
    }
    public void NextScene () {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene() {
        fadeTransition.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
        fadeTransition.SetTrigger("Start");
    }
}
