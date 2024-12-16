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

        // 
        rb.drag = stats.GroundDrag;
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
        CheckForSprint();
        LimitVelocity();
    }
    public override void DoFixedUpdateState()
    {
        base.DoFixedUpdateState();
        // Adds a force to the player in the direction they are pressing relative to the camera
        rb.AddForce((orientation.forward * playerInput.yInput + orientation.right * playerInput.xInput).normalized * stats.GroundAcceleration * 100f);
        LimitVelocity();
    }

    /// <summary>
    /// Check if player is sprinting
    /// </summary>
    private void CheckForSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = stats.MaxSprintSpeed;
            player.animator.SetBool("Sprint", true);
        }
        else
        {
            maxSpeed = stats.maxSpeed;
            player.animator.SetBool("Sprint", false);
        }
    }
    
    /// <summary>
    /// Limits the player's horizontal/flat velocity (velocity in x and z axis)
    /// </summary>
    private void LimitVelocity()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

}
