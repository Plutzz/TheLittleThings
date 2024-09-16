using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NavigateState : State
{
    public Vector2 destination;

    public Transform player;

    public float detectionRadius = 10f;

    public float speed = 1;
    public float threshold = 0.1f;

    // Generic animation state?
    // public State animation

    [SerializeField] private AnimationClip animClip;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play(animClip.name);
    }


    public override void DoUpdateState()
    {
        base.DoUpdateState();
        if(Vector2.Distance(core.transform.position, player.position) <= detectionRadius)
        {
            destination = player.position;
        }
        else
        {
            isComplete = true; 
            return;
        }
        if (Vector2.Distance(core.transform.position, destination) < threshold)
        {
            rb.velocity = Vector2.zero;  
            isComplete = true;
            return;
        }

        Vector2 direction = (destination - (Vector2)core.transform.position).normalized;
        core.transform.localScale = new Vector3(Mathf.Sign(direction.x), core.transform.localScale.y, core.transform.localScale.z);
    }

    public override void DoFixedUpdateState()
    {
        base.DoFixedUpdateState();
        Vector2 direction = (destination - (Vector2) core.transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }
}
