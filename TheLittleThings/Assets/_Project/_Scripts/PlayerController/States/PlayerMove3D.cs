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
    private float acceleration;
    private PlayerStats stats => player.stats;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        player.SetTrigger("Walk");
        rb.drag = stats.GroundDrag;
        player.ChangeGravity(stats.GroundGravity);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        player.animator.SetBool("Sprint", false);
        animator.Play("Walk");
        player.ChangeGravity(stats.NormalGravity);
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
        RaycastHit hit;
        Physics.Raycast(orientation.position, Vector3.down, out hit, 1);

        Vector3 forwardOriented = Vector3.Cross(orientation.right, hit.normal).normalized;
        Vector3 rightOriented = Vector3.Cross(hit.normal, forwardOriented).normalized;
        // Adds a force to the player in the direction they are pressing relative to the camera
        rb.AddForce((forwardOriented * playerInput.yInput + rightOriented * playerInput.xInput).normalized * (acceleration * 100f));
        LimitVelocity();
    }

    /// <summary>
    /// Check if player is sprinting
    /// </summary>
    private void CheckForSprint()
    {
        // TODO: Create sprint input and replace if condition
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = stats.MaxSprintSpeed;
            acceleration = stats.SprintAcceleration;
            player.animator.SetBool("Sprint", true);
        }
        else
        {
            maxSpeed = stats.MaxSpeed;
            acceleration = stats.WalkAcceleration;
            player.animator.SetBool("Sprint", false);
        }
    }
    
    /// <summary>
    /// Limits the player's horizontal/flat velocity (velocity in x and z axis)
    /// </summary>
    private void LimitVelocity()
    {
        
        RaycastHit hit;
        Physics.Raycast(orientation.position, Vector3.down, out hit, 1);
        Vector3 flatVel = Vector3.ProjectOnPlane(rb.velocity, hit.normal);
        
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            Vector3 verticalVel = rb.velocity - flatVel;
            rb.velocity = limitedVel + verticalVel;
        }
        
        
    }

}
