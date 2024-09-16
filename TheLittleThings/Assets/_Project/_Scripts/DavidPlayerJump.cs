using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DavidPlayerJump : MonoBehaviour
{
    public float acceleration = 20f;
    public float jumpForce = 20f;
    public float gravityOnWall = 1f;
    public float normalGravity = 5f;
    public float wallJumpMagnitude = 3f;
    public float downwardForce = 5f;
    public bool wallJump = false;

    private bool onWall = false;
    public bool jumpReady = true;
    private float wallJumpForce = 0f;
    private Rigidbody2D rb;
    private bool grounded = true;
    private bool jumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (jumpReady == true && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //creates variable jump, adds downward force if player lets go of space making character fall faster leading to smaler jump
        if (jumping && !Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(-Vector2.up * downwardForce);
        }
    }

    private void Jump()
    {
        jumping = true;
        jumpReady = false;

        if (grounded || !onWall)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }
        else
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(wallJumpForce, jumpForce / 1.5f), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpReady = true;
            grounded = true;
            rb.gravityScale = 5f;
            jumping = false;
            wallJump = false;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector2.right * rb.velocity.x;

            jumpReady = true;
            jumping = false;

            float wallSide = transform.position.x - collision.gameObject.transform.position.x;

            if (wallSide < 0)
            {
                wallJumpForce = -wallJumpMagnitude;
            }
            else
            {
                wallJumpForce = wallJumpMagnitude;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (!grounded && rb.velocity.y < 0)
            {
                rb.gravityScale = gravityOnWall;
                onWall = true;
                jumpReady = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallJump = true;
            onWall = false;
            rb.gravityScale = 5f;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

}
