using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborne3D : State
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Player player;
    [SerializeField] private Transform orientation;
    private PlayerStats stats => player.stats;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //animator.Play("Fall");
        rb.drag = 0;
    }

    public override void DoFixedUpdateState()
    {
        rb.velocity = (new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -stats.FallSpeedLimit, stats.FallSpeedLimit), rb.velocity.z));
        //checks if player wants to move in the opposite direction of velocity
        // NOTE: feels a little touchy, maybe instead of instantly setting velo to 0 we make it so that
        // if the player isn't holding the direction they are moving in we add a decel force
        // (so if you aren't pressing an input then the player will start heading towards 0 velocity)

        rb.AddForce((orientation.forward * playerInput.yInput + orientation.right * playerInput.xInput).normalized * (stats.Acceleration));
    }
}
