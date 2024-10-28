using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class IdleState : State
{
    public float IdleTime = 2f;
    [SerializeField] private AnimationClip animClip;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //animator.Play(animClip.name);
    }
    public override void CheckTransitions()
    {
        base.CheckTransitions();

        if (true/*stateUptime > animClip.length*/)
        {
            isComplete = true;
        }

    }
    public override void DoUpdateState()
    {
        base.DoUpdateState();
    }

}
