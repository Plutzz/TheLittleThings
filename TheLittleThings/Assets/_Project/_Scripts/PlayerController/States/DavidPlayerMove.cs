using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DavidPlayerMove : MonoBehaviour
{
    public float acceleration = 20f;
    public float maxSpeed = 10f;
    public float jumpForce = 6f;
    public float gravityOnWall = 1f;
    public float normalGravity = 5f;
    public float wallJumpMagnitude = 3f;

    private bool onWall = false;
    public bool jumpReady = true;
    private float wallJumpForce = 0f;
    private Rigidbody2D rb;
    private bool grounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (jumpReady == true && Input.GetKeyDown(KeyCode.Space))
        {
            jumpReady = false;

            if(grounded || !onWall)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            } else
            {
                rb.AddForce(new Vector2 (wallJumpForce/2f, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (Mathf.Abs(rb.velocity.x) < (maxSpeed))
        {
            xMove();
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
        }
    }

    private void xMove()
    {
        if (onWall && wallJumpForce < -0.2)
        {
            rb.AddForce(Vector2.right * Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -1, 0) * acceleration);
        } else if(onWall && wallJumpForce > 0.2)
        {
            rb.AddForce(Vector2.right * Mathf.Clamp(Input.GetAxisRaw("Horizontal"), 0, 1) * acceleration);
        } else
        {
            rb.AddForce(Vector2.right * Input.GetAxisRaw("Horizontal") * acceleration);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            rb.gravityScale = 5f;
        }

        jumpReady = true;

        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector2.right * rb.velocity.x;

            jumpReady = true;

            float wallSide = transform.position.x - collision.gameObject.transform.position.x;

            if(wallSide < 0)
            {
                wallJumpForce = -wallJumpMagnitude;
            } else
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
            onWall = false;
            rb.gravityScale = 5f;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

}