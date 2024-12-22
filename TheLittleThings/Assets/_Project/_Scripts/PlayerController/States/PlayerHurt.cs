using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : State
{
    [SerializeField] private float hurtDuration = 0.5f;
    [SerializeField] private AnimationClip animClip;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play(animClip.name);
        isComplete = false;
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        if (stateUptime >= hurtDuration) {
            isComplete = true;
        }  
    }
}
