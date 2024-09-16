using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpManager : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Player player;
    [SerializeField] private PlayerInput playerInput;
    private PlayerStats playerStats => player.stats;
    [SerializeField] private GroundSensor groundSensor;

    float FrameBufferNum => playerStats.JumpFrameBufferAmount;
    float JumpForce => playerStats.JumpForce;

    private int framesSinceLastSpacebar, framesSinceOnGround;

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
        
    }

    void FixedUpdate()
    {
        if (framesSinceOnGround == -1)
        {
            framesSinceOnGround = (int)FrameBufferNum;
        }
        else if (!(player.stateMachine.currentState is PlayerAirborne) && groundSensor.grounded)
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
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);

            framesSinceLastSpacebar = (int)FrameBufferNum; // ensure two jumps don't happen off one input
            framesSinceOnGround = -1; // magic™ (look at fixed update)
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
