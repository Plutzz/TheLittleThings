using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFlipState : State
{
    [SerializeField] private AnimationClip animClip;
    [SerializeField] private AnimationClip nextanimClip;
    [SerializeField] private Transform toFlip;

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
        toFlip.eulerAngles += 180f * Vector3.up;
        animator.Play(nextanimClip.name);
    }
}
