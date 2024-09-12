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
        animator.Play(animClip.name);
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        rb.velocity = new Vector2(chargeSpeed * Mathf.Sign(core.transform.localScale.x), rb.velocity.y);
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
