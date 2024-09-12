using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpManager : MonoBehaviour
{
    public float JumpForce;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Player player;
    public uint FrameBufferNum;

    private uint m_ticksSinceLastSpacebar, m_ticksSinceOnGround;

    private void Awake()
    {
        m_ticksSinceLastSpacebar = FrameBufferNum; // ensure player doesn't jump on start
        m_ticksSinceOnGround = FrameBufferNum;
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

            m_ticksSinceLastSpacebar = FrameBufferNum; // ensure two jumps don't happen off one input
            m_ticksSinceOnGround = FrameBufferNum;
        }

        
    }
}
