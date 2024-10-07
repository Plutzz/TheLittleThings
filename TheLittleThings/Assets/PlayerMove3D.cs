using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove3D : State
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Player player;
    private PlayerStats stats => player.stats;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //animator.Play("Run");
        rb.drag = 4;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoFixedUpdateState()
    {
        //if velocity is less than maxspeed
        if (Mathf.Abs(rb.velocity.magnitude) < stats.MaxSpeed)
        {
            rb.AddForce(new Vector3 (playerInput.xInput, 0, playerInput.zInput).normalized * stats.Acceleration);
        }
        //else //if at max speed, set velocity to max speed for consistent movement
        //{
        //    if (rb.velocity.x < -stats.MaxSpeed)
        //    {
        //        rb.velocity = new Vector2(Mathf.Clamp(-stats.MaxSpeed, -stats.MaxSpeed, 0), rb.velocity.y);
        //    }
        //    else if (rb.velocity.x > stats.MaxSpeed)
        //    {
        //        rb.velocity = new Vector2(Mathf.Clamp(stats.MaxSpeed, 0, stats.MaxSpeed), rb.velocity.y);
        //    }
        //}
    }

}
