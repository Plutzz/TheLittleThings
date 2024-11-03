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
        Debug.Log("airborne state enter");
        animator.SetTrigger("Jump");
        //animator.Play("Jump");
        rb.drag = 0;
        //Clamp fall speed
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -stats.FallSpeedLimit, stats.FallSpeedLimit), rb.velocity.z);
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flatVel.magnitude > stats.MaxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * stats.MaxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public override void DoFixedUpdateState()
    {
        rb.AddForce((orientation.forward * playerInput.yInput + orientation.right * playerInput.xInput).normalized * (stats.AirAcceleration));
    }
}
