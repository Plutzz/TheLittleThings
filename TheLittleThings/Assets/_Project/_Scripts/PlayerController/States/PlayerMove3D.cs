using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;

public class PlayerMove3D : State
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Player player;
    [SerializeField] private Transform orientation;

    private float maxSpeed;
    private PlayerStats stats => player.stats;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        player.SetTrigger("Walk");
        //animator.Play("Walk");
        rb.drag = 4;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        player.animator.SetBool("Sprint", false);
        animator.Play("Walk");
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    public override void DoFixedUpdateState()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = stats.SprintSpeed;
            player.animator.SetBool("Sprint", true);
        }
        else
        {
            maxSpeed = stats.MaxSpeed;
            player.animator.SetBool("Sprint", false);
        }
        rb.AddForce((orientation.forward * playerInput.yInput + orientation.right * playerInput.xInput).normalized * stats.GroundAcceleration);        
    }

}
