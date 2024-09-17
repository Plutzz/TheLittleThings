using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DavidPlayerMove : MonoBehaviour
{
    public float acceleration = 20f;
    public float airAcceleration = 50f;
    public float maxSpeed = 10f;
    public float jumpForce = 10f;
    public float gravityOnWall = 1f;
    public float normalGravity = 5f;
    public float wallJumpMagnitude = 3f;
    public float downwardForce = 5f;
    public bool wallJump = false;
    
    private bool onWall = false;
    public bool jumpReady = true; //keeps track of when player touches ground or wall since they can jump on both
    private float wallJumpForce = 0f;
    private Rigidbody2D rb;
    private bool grounded = true;
    private bool jumping = false; //basically if airborne

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
    }

    private void Jump()
    {
        jumping = true; //airborne
        jumpReady = false;

       
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        //air strafe handling
        //if moving while airborne from a normal jump and not a walljump
        if (!grounded && !onWall && rb.velocity.x != 0 && !wallJump)
        {
            //checks if player wants to move in the opposite direction of velocity
            if(Input.GetAxisRaw("Horizontal") != 0 && Mathf.Sign(rb.velocity.x) != Input.GetAxisRaw("Horizontal"))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpReady = true;
            jumping = false;
            grounded = true;
            rb.gravityScale = normalGravity;
            wallJump = false;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector2.right * rb.velocity.x;

            jumpReady = true;
            jumping = false;

            //calculates what side the wall is on for walljump to push player off the wall using walljumpforce
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
            //if touching a wall and not touching the ground, change gravity to give sliding down effect
            //check if y velocity < 0 so that player doesnt have less gravity when normal jumping off the ground while touching a wall
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
            rb.gravityScale = normalGravity; //turns gravity back to normals when leaving wall
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

}