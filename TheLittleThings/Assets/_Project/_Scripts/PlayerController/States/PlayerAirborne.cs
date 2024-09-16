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
        
    }
}
