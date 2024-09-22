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
        animator.Play(animClip.name);
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        float distance = target.position.x - core.transform.position.x;
        rb.velocity = new Vector2(Mathf.Sign(distance) * speed, rb.velocity.y);

        core.transform.localScale = new Vector3(-1 * Mathf.Sign(distance) * Mathf.Abs(core.transform.localScale.x), core.transform.localScale.y, core.transform.localScale.z);

        if (Mathf.Abs(distance) < arriveThreshold)
        {
            isComplete = true;
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        rb.velocity = Vector2.zero;
    }
}
