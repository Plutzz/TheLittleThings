using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpManager : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Player player;
    [SerializeField] private PlayerStats playerStats;

    float FrameBufferNum => playerStats.FrameBufferNum;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_ticksSinceLastSpacebar = 0;
        }
    }

    void FixedUpdate()
    {
        if (!(player.stateMachine.currentState is PlayerAirborne))
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
            m_ticksSinceOnGround = (int)FrameBufferNum;
        }

        
    }
}
