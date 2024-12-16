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

        RaycastHit hit;
        Physics.Raycast(orientation.position, Vector3.down, out hit, 1);

        Vector3 forwardOriented = Vector3.Cross(orientation.right, hit.normal).normalized;
        Vector3 rightOriented = Vector3.Cross(hit.normal, forwardOriented).normalized;

        rb.AddForce((forwardOriented * playerInput.yInput + rightOriented * playerInput.xInput).normalized * stats.GroundAcceleration);
    }

}
