using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : State
{
    [SerializeField] private AnimationClip animClip;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float chargeTime = 2f;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Enter Charge");
        //animator.Play(animClip.name);
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        rb.velocity = new Vector3(0, rb.velocity.y, 0) + core.transform.forward * chargeSpeed;
        core.transform.forward = rb.velocity.normalized;
    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();
        if(stateUptime > chargeTime)
        {
            isComplete = true;
        }
    }
}
