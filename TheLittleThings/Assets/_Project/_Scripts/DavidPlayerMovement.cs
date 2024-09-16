using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DavidPlayerMovement : MonoBehaviour
{
    public float acceleration = 20f;
    public float maxSpeed = 10f;
    public float jumpForce = 10f;
    public float wallJumpMagnitude = 3f;
    public bool wallJump = false;

    private bool onWall = false;
    public bool jumpReady = true;
    private float wallJumpForce = 0f;
    private Rigidbody2D rb;
    private bool grounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        //air strafe handling
        if (!grounded && !onWall && rb.velocity.x != 0 && !wallJump)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 && Mathf.Sign(rb.velocity.x) != Input.GetAxisRaw("Horizontal"))
            {
                Debug.Log("Strafe");
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        if (Mathf.Abs(rb.velocity.x) < (maxSpeed))
        {
            if (onWall && wallJumpForce < 0)
            {
                rb.AddForce(Vector2.right * Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -1, 0) * acceleration);
            }
            else if (onWall && wallJumpForce > 0)
            {
                rb.AddForce(Vector2.right * Mathf.Clamp(Input.GetAxisRaw("Horizontal"), 0, 1) * acceleration);
            }
            else
            {
                rb.AddForce(Vector2.right * Input.GetAxisRaw("Horizontal") * acceleration);
            }
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
        }
    }
}
