using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpManager : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Player player;
    [SerializeField] private PlayerInput playerInput;
    private PlayerStats playerStats => player.stats;
    [HorizontalLine]
    [Header("Sensors")]
    [SerializeField] private GroundSensor groundSensor;
    [SerializeField] private WallSensor wallSensor;

    float FrameBufferNum => playerStats.JumpFrameBufferAmount;
    float JumpForce => playerStats.JumpForce;
    float downwardForce = 5f;

    private int framesSinceLastSpacebar, framesSinceOnGround;
    private bool jumping;

    private void Awake()
    {
        framesSinceLastSpacebar = (int)FrameBufferNum; // ensure player doesn't jump on start
        framesSinceOnGround = (int)FrameBufferNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.jumpPressedThisFrame)
        {
            framesSinceLastSpacebar = 0;
        }

        //creates variable jump, adds downward force if player lets go of space making character fall faster leading to smaler jump
        if (jumping && !playerInput.jumpHeld)
        {
            rb.AddForce(-Vector2.up * downwardForce);
        }

        if(groundSensor.grounded && rb.velocity.y < 0)
        {
            jumping = false;
        }

    }

    void FixedUpdate()
    {
        if (framesSinceOnGround == -1)
        {
            framesSinceOnGround = (int)FrameBufferNum;
        }
        else if (player.stateMachine.currentState is not PlayerAirborne && (groundSensor.grounded || wallSensor.wallRight || wallSensor.wallLeft))
        {
            framesSinceOnGround = 0;
        }
        else
        {
            framesSinceOnGround++;
        }
        framesSinceLastSpacebar++;

        if (framesSinceLastSpacebar < FrameBufferNum)
        {
            AttemptJump();
        }
    }

    void AttemptJump()
    {
        if (framesSinceOnGround < FrameBufferNum)
        {

            if (wallSensor.wallLeft && !groundSensor.grounded) //walljump
            {
                rb.velocity = Vector2.zero; //set to zero before jump for consistent walljump

                rb.AddForce(new Vector2(playerStats.JumpForce, playerStats.JumpForce / 1.5f), ForceMode.Impulse); //adds force x=wallJumpForce y=jumpforce/1.5
            }
            else if (wallSensor.wallRight && !groundSensor.grounded)
            {
                rb.velocity = Vector2.zero; //set to zero before jump for consistent walljump

                rb.AddForce(new Vector2(-playerStats.JumpForce, playerStats.JumpForce / 1.5f), ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.up * playerStats.JumpForce, ForceMode.Impulse);
            }

            jumping = true;

            framesSinceLastSpacebar = (int)FrameBufferNum; // ensure two jumps don't happen off one input
            framesSinceOnGround = -1; // magic� (look at fixed update)
        }
    }

    private void OnDrawGizmos()
    {
        #if UNITY_EDITOR
        if (Application.isPlaying)
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;
            UnityEditor.Handles.Label(transform.position + Vector3.up * 2.5f, $"Ticks since: Spacebar: {framesSinceLastSpacebar} Ground: {framesSinceOnGround}", style);

        }
        #endif
    }
}
