using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.KeyCode;

public class PlayerInput : MonoBehaviour
{
    public float xInput { get;  private set; }
    public bool jumpPressedThisFrame { get; private set; }
    public bool jumpHeld { get; private set; }
    public bool attackPressedThisFrame { get; private set; }

    public bool ResetInput
    {
        get; private set;
    }
    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        jumpPressedThisFrame = Input.GetKeyDown(KeyCode.Space);
        jumpHeld = Input.GetKey(KeyCode.Space);
        attackPressedThisFrame = Input.GetMouseButton(0);
        ResetInput = Input.GetKeyDown(R);
    }
}
