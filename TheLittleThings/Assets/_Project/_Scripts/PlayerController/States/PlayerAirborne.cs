using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborne : State
{
    [SerializeField] private PlayerInput playerInput;
    //[SerializeField] private PlayerStats playerStats;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 50f;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play("Fall");
        rb.drag = 0;
    }

    public override void DoFixedUpdateState()
    {
        //checks if player wants to move in the opposite direction of velocity
        if (rb.velocity.x != 0 && Input.GetAxisRaw("Horizontal") != 0 && Mathf.Sign(rb.velocity.x) != Input.GetAxisRaw("Horizontal"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //if velocity is less than maxspeed
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            ////movement on wall if the wall is on the right, doesn't let player add force against wall because then they will stop moving since the wall has a friction of 0.4
            //if (onWall && wallJumpForce < 0)
            //{
            //    rb.AddForce(Vector2.right * Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -1, 0) * acceleration);
            //}
            ////movement on the wall if the wall is on the left
            //else if (onWall && wallJumpForce > 0)
            //{
            //    rb.AddForce(Vector2.right * Mathf.Clamp(Input.GetAxisRaw("Horizontal"), 0, 1) * acceleration);
            //}
            rb.AddForce(Vector2.right * Input.GetAxisRaw("Horizontal") * acceleration);
        }
        else //if at max speed, set velocity to max speed for consistent movement
        {
            if (rb.velocity.x < -maxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Clamp(-maxSpeed, -maxSpeed, 0), rb.velocity.y);
            }
            else if (rb.velocity.x > maxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Clamp(maxSpeed, 0, maxSpeed), rb.velocity.y);
            }
        }
    }
}
