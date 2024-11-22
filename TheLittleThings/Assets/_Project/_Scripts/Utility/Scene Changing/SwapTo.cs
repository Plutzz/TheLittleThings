using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapTo : MonoBehaviour
{
    public string SceneToSwapTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SwapToScene()
    {
        SceneTransitionManager.Instance.LoadScene(SceneToSwapTo);
    }
}
