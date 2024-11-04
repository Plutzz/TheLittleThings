using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NavigateState : State
{
    [SerializeField] private Transform destination;

    public float speed;
    public float turnSpeed;
    public float threshold;

    [SerializeField] private float minTime = 0.5f;

    [SerializeField] private AnimationClip animClip;
    private Vector3 direction;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play(animClip.name);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        rb.velocity = Vector3.zero;
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        Debug.Log((core.transform.position - destination.position).sqrMagnitude);

        if(stateUptime > minTime && (core.transform.position - destination.position).sqrMagnitude < threshold * threshold)
        {
            isComplete = true;
        }

        core.transform.forward = Vector3.RotateTowards(core.transform.forward, direction, turnSpeed * Time.deltaTime, 0);
        rb.velocity = new Vector3(0, rb.velocity.y, 0) + direction * speed * math.max(Vector3.Dot(direction, core.transform.forward), 0);
    }

    public override void DoFixedUpdateState()
    {
        base.DoFixedUpdateState();
        direction = (destination.position - core.transform.position);
        direction.y = 0;
        direction.Normalize(); 
    }
}
