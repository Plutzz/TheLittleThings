using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTowardsState : State
{
    
    [SerializeField] private Transform target;
    [SerializeField] private bool flipDirection;
    [SerializeField] private float threshold = 1f;
    [SerializeField] private float turnSpeed = 5;
    private Vector3 direction;
    public override void DoUpdateState()
    {
        base.DoUpdateState();
        direction = (target.position - core.transform.position);
        direction.y = 0;
        if(flipDirection)
        {
            direction *= -1;
        }
        direction.Normalize();
        core.transform.forward = Vector3.RotateTowards(core.transform.forward, direction, turnSpeed * Time.deltaTime, 0);
        if (Vector3.Angle(core.transform.forward, direction) < threshold)
        {
            isComplete = true;
        }
    }
}
