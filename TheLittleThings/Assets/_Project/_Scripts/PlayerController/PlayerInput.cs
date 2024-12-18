using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.KeyCode;

public class PlayerInput : MonoBehaviour
{
    public float xInput { get;  private set; }
    public float yInput { get; private set; }
    public Vector2 moveVector { get; private set; }
    public float timeOfLastMoveInput { get; private set; }
    public bool jumpPressedThisFrame { get; private set; }
    public bool jumpReleasedThisFrame { get; private set; }
    public bool jumpHeld { get; private set; }
    public bool attackPressedDownThisFrame { get; private set; }
    public bool attackReleasedThisFrame { get; private set; }
    public bool rollPressedThisFrame { get; private set; }
    public bool sprintHeld { get; private set; }
    
    

    public bool ResetInput
    {
        get; private set;
    }
    // Update is called once per frame
    void Update()
    {
        // Handle move inputs
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        moveVector = new Vector2(xInput, yInput);
        
        if (moveVector.magnitude > 0)
        {
            timeOfLastMoveInput = Time.time;
        }
        
        
        jumpPressedThisFrame = Input.GetKeyDown(KeyCode.Space);
        jumpReleasedThisFrame = Input.GetKeyUp(KeyCode.Space);
        rollPressedThisFrame = Input.GetKeyDown(KeyCode.LeftControl);
        jumpHeld = Input.GetKey(KeyCode.Space);
        attackPressedDownThisFrame = Input.GetMouseButtonDown(0);
        attackReleasedThisFrame = Input.GetMouseButtonUp(0);
        ResetInput = Input.GetKeyDown(R);
        sprintHeld = Input.GetKey(LeftShift);
    }
}
