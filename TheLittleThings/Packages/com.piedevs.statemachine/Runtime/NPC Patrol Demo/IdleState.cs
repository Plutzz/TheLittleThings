using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class IdleState : State
{
    public float idleTime = 2f;
    [SerializeField] private AnimationClip animClip;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        rb.velocity = Vector3.zero;
        animator.Play(animClip?.name);
    }
    public override void CheckTransitions()
    {
        base.CheckTransitions();

        if (stateUptime > idleTime)
        {
            isComplete = true;
        }

    }
    public override void DoUpdateState()
    {
        base.DoUpdateState();
    }

}
