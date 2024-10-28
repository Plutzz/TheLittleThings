using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NavigateState : State
{
    [DoNotSerialize] public System.Func<Vector3> destination;
    [SerializeField] private EnemyChooseRandom attackState;

    public float speed;
    public float turnSpeed;
    public float threshold;

    // Generic animation state?
    // public State animation

    [SerializeField] private AnimationClip animClip;
    private Vector3 direction;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //animator.Play(animClip.name);
    }


    public override void DoUpdateState()
    {
        base.DoUpdateState();
        if((core.transform.position - destination.Invoke()).sqrMagnitude < threshold * threshold)
        {
            isComplete = true;
            stateMachine.SetState(attackState);
        }

        core.transform.forward = Vector3.RotateTowards(core.transform.forward, direction, turnSpeed * Time.deltaTime, 0);
        rb.velocity = new Vector3(0, rb.velocity.y, 0) + direction * speed * math.max(Vector3.Dot(direction, core.transform.forward), 0);
    }

    public override void DoFixedUpdateState()
    {
        base.DoFixedUpdateState();
        direction = (destination.Invoke() - core.transform.position);
        direction.y = 0;
        direction.Normalize();
        
    }
}
