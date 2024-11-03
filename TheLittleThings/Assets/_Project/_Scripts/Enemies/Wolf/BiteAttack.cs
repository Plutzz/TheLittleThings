using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAttack : State
{
    [SerializeField] private AnimationClip animClip;
    [SerializeField] private float stateTime;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //animator.Play(animClip.name);
    }
    public override void CheckTransitions()
    {
        base.CheckTransitions();
        if (stateUptime > stateTime)
        {
            isComplete = true;
        }
    }
}
