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
    public Vector2 lookVector { get; private set; }
    public float timeOfLastMoveInput { get; private set; }
    public bool jumpPressedThisFrame { get; private set; }
    public bool jumpReleasedThisFrame { get; private set; }
    public bool jumpHeld { get; private set; }
    public bool attackPressedDownThisFrame { get; private set; }
    public bool attackHeld { get; private set; }
    public bool attackReleasedThisFrame { get; private set; }
    public bool rollPressedThisFrame { get; private set; }
    public bool sprintHeld { get; private set; }
    public bool sprintPressedThisFrame { get; private set; }
    public bool toggleCameraPressedDownThisFrame { get; private set; }
    public bool toggleCameraHeld { get; private set; }
    public bool toggleCameraReleasedThisFrame { get; private set; }
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
        
        
        // Jump
        jumpPressedThisFrame = playerActionMap.FindAction("Jump").WasPerformedThisFrame();
        jumpReleasedThisFrame = playerActionMap.FindAction("Jump").WasReleasedThisFrame();
        jumpHeld = playerActionMap.FindAction("Jump").ReadValue<float>() > 0;

        // Roll
        rollPressedThisFrame = playerActionMap.FindAction("Roll").WasPerformedThisFrame();
        
        // Attack
        attackPressedDownThisFrame = playerActionMap.FindAction("Attack").WasPerformedThisFrame();
        attackHeld = playerActionMap.FindAction("Attack").ReadValue<float>() > 0;
        attackReleasedThisFrame = playerActionMap.FindAction("Attack").WasReleasedThisFrame();
        
        // Sprint
        sprintHeld = playerActionMap.FindAction("Sprint").ReadValue<float>() > 0;
        sprintPressedThisFrame = playerActionMap.FindAction("Attack").WasPerformedThisFrame();
        
        // Toggle Camera
        toggleCameraPressedDownThisFrame = playerActionMap.FindAction("Toggle Camera").WasPerformedThisFrame();
        toggleCameraHeld = playerActionMap.FindAction("Toggle Camera").ReadValue<float>() > 0;
        toggleCameraReleasedThisFrame = playerActionMap.FindAction("Toggle Camera").WasReleasedThisFrame();
        
        // DEBUG INPUTS
        ResetInput = Input.GetKeyDown(R);
    }
}
