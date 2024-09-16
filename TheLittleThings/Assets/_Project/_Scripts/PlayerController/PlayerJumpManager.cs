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

    private int m_ticksSinceLastSpacebar, m_ticksSinceOnGround;

    private void Awake()
    {
        m_ticksSinceLastSpacebar = (int)FrameBufferNum; // ensure player doesn't jump on start
        m_ticksSinceOnGround = (int)FrameBufferNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.jumpPressedThisFrame)
        {
            m_ticksSinceLastSpacebar = 0;
        }
        
    }

    void FixedUpdate()
    {
        if (m_ticksSinceOnGround == -1)
        {
            m_ticksSinceOnGround = (int)FrameBufferNum;
        }
        else if (!(player.stateMachine.currentState is PlayerAirborne) && groundSensor.grounded)
        {
            m_ticksSinceOnGround = 0;
        }
        else
        {
            m_ticksSinceOnGround++;
        }
        m_ticksSinceLastSpacebar++;

        if (m_ticksSinceLastSpacebar < FrameBufferNum)
        {
            AttemptJump();
        }
    }

    void AttemptJump()
    {
        if (m_ticksSinceOnGround < FrameBufferNum)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);

            m_ticksSinceLastSpacebar = (int)FrameBufferNum; // ensure two jumps don't happen off one input
            m_ticksSinceOnGround = -1; // magic™ (look at fixed update)
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
            UnityEditor.Handles.Label(transform.position + Vector3.up * 2.5f, $"Ticks since: Spacebar: {m_ticksSinceLastSpacebar} Ground: {m_ticksSinceOnGround}", style);

        }
        #endif
    }
}
