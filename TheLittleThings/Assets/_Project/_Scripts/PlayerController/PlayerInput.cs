using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using static UnityEngine.KeyCode;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerInputAction;
    public Vector2 moveVector { get; private set; }
    public float timeOfLastMoveInput { get; private set; }
    public bool jumpPressedThisFrame { get; private set; }
    public bool jumpReleasedThisFrame { get; private set; }
    public bool jumpHeld { get; private set; }
    public bool attackPressedDownThisFrame { get; private set; }
    public bool attackHeld { get; private set; }
    public bool attackReleasedThisFrame { get; private set; }
    public bool rollPressedThisFrame { get; private set; }
    public bool sprintHeld { get; private set; }
    public bool ResetInput { get; private set;}

    private void OnEnable()
    {
        playerInputAction.Enable();
    }

    private void OnDisable()
    {
        playerInputAction.Disable();
    }


    void Update()
    {
        InputActionMap playerActionMap = playerInputAction.actionMaps[0];
        moveVector = playerActionMap.FindAction("Move").ReadValue<Vector2>();
        
        if (moveVector.magnitude > 0)
        {
            timeOfLastMoveInput = Time.time;
        }
        
        
        jumpPressedThisFrame = Input.GetKeyDown(KeyCode.Space);
        jumpReleasedThisFrame = Input.GetKeyUp(KeyCode.Space);
        rollPressedThisFrame = Input.GetKeyDown(KeyCode.LeftControl);
        jumpHeld = Input.GetKey(KeyCode.Space);
        attackPressedDownThisFrame = Input.GetMouseButtonDown(0);
        attackHeld = Input.GetMouseButton(0);
        attackReleasedThisFrame = Input.GetMouseButtonUp(0);
        ResetInput = Input.GetKeyDown(R);
        sprintHeld = Input.GetKey(LeftShift);
    }
}
