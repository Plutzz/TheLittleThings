using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationState : State
{
    [SerializeField] private AnimationClip animClip;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play(animClip.name);
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();
        if(stateUptime > animClip.length)
        {
            isComplete = true;
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
}
