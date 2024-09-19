using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborne : State
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Player player;
    public float decceleration = 10;
    private PlayerStats stats => player.stats;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play("Fall");
        rb.drag = 0;
    }

    public override void DoFixedUpdateState()
    {
        //checks if player wants to move in the opposite direction of velocity
        // NOTE: feels a little touchy, maybe instead of instantly setting velo to 0 we make it so that
        // if the player isn't holding the direction they are moving in we add a decel force
        // (so if you aren't pressing an input then the player will start heading towards 0 velocity)
        if (rb.velocity.x != 0 && playerInput.xInput != 0 && Mathf.Sign(rb.velocity.x) != Mathf.Sign(playerInput.xInput))
        {
            rb.AddForce(Vector2.right * playerInput.xInput * (stats.Acceleration));
            //rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (rb.velocity.x != 0 && playerInput.xInput == 0)
        {
            Debug.Log("Decceleration");
            rb.AddForce(Vector2.right * -Mathf.Sign(rb.velocity.x) * decceleration);
        }

        //if velocity is less than maxspeed
        if (Mathf.Abs(rb.velocity.x) < stats.MaxSpeed)
        {
            rb.AddForce(Vector2.right * playerInput.xInput * stats.Acceleration);
        }
        else //if at max speed, set velocity to max speed for consistent movement
        {
            if (rb.velocity.x < -stats.MaxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Clamp(-stats.MaxSpeed, -stats.MaxSpeed, 0), rb.velocity.y);
            }
            else if (rb.velocity.x > stats.MaxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Clamp(stats.MaxSpeed, 0, stats.MaxSpeed), rb.velocity.y);
            }
        }
    }
}
