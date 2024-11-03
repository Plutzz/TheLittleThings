using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationState : State
{
    [SerializeField] private AnimationClip animClip;
    //[SerializeField] private float chargeTime = 2f;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Enter Charge");
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
}
