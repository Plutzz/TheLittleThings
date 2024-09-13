using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborne : State
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerStats playerStats;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play("Fall");
    }

    public override void DoFixedUpdateState()
    {
        if (playerInput.xInput == 0)
        {
            rb.AddForce(new Vector2(-rb.velocity.x * playerStats.NoInputDeceleration / 10, 0), ForceMode2D.Impulse);
        }
        float inputDir = 0;

        if (playerInput.xInput > 0)
        {
            inputDir = 1;
        }
        else if (playerInput.xInput < 0)
        {
            inputDir = -1;
        }

        if (inputDir * rb.velocity.x < playerStats.MaxSpeed)
        {
            float diff = (float)Math.Min(0.05, Math.Abs(inputDir * playerStats.MaxSpeed - rb.velocity.x) / 20);

            //Debug.Log($"dir: {inputDir}\tspd: {playerStats.MaxSpeed}\tvel: {rb.velocity.x}\tdiff: {diff}");

            rb.AddForce(new Vector2(playerInput.xInput * diff, 0), ForceMode2D.Impulse);
        }
    }
}
