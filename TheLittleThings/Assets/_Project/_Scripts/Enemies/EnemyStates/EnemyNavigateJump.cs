using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigateJump : State
{
    [SerializeField] private AnimationClip animClip;
    public Transform target;
    public float speed = 5f;
    public float arriveThreshold = 2f;

    private float destination;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        destination = target.position.x;
        //animator.Play(animClip.name);
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        Vector3 horizontalDist = rb.position - target.position;
        horizontalDist.y = 0;
        rb.velocity = new Vector3(0, rb.velocity.y, 0) + horizontalDist.normalized;

        core.transform.forward = horizontalDist.normalized;

        if (Mathf.Abs(horizontalDist.sqrMagnitude) < arriveThreshold * arriveThreshold)
        {
            isComplete = true;
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        rb.velocity = Vector3.zero;
    }
}
