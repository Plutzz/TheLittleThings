using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        Debug.Log("Button has been clicked");
    }

}
