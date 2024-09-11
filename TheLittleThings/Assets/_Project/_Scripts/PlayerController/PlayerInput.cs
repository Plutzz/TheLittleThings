using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float xInput { get;  private set; }
    public bool jumpPressedThisFrame { get; private set; }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        jumpPressedThisFrame = Input.GetKeyDown(KeyCode.Space);
    }
}
