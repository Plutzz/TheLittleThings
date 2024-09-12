using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : State
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerStats playerStats;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play("Run");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoUpdateState()
    {
        if (playerInput.xInput == 0)
        {
            rb.AddForce(new Vector2(-rb.velocity.x * playerStats.NoInputDeceleration, 0), ForceMode2D.Impulse);
        }
        if (Math.Abs(rb.velocity.x) < playerStats.MaxSpeed)
        {
            float diff = playerStats.MaxSpeed - Math.Abs(rb.velocity.x);
            rb.AddForce(new Vector2(playerInput.xInput * diff, 0), ForceMode2D.Impulse);
        }
    }

}
