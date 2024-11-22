using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JumpToPoint : State
{
    private Rigidbody target;   // Player grabbed from Frog file
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float displacementOffset;
    [SerializeField] private float predictionAmount;
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float turnSpeed = 5;
    [SerializeField] private float groundCheckLength = 2.25f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AnimationClip animClip;

    private Vector3 direction;
    private float v0;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        rb.useGravity = false;
        Vector3 distanceVector = target.position + (Vector3.ClampMagnitude(target.velocity, playerStats.MaxSpeed) * predictionAmount) - transform.position;
        float displacement = Vector3.ProjectOnPlane(distanceVector, Vector3.up).magnitude - displacementOffset;
        v0 = Mathf.Sqrt(Mathf.Abs(displacement) * gravity * 2);
        Debug.Log("Displacement " + displacement + " V0 " + v0);
        rb.velocity = v0 * Vector3.ProjectOnPlane(distanceVector, Vector3.up).normalized / 2 + v0 * transform.up / 2;
        animator.Play(animClip.name);
        animator.StartPlayback();
        animator.playbackTime = 0;
        animator.speed = 0;
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();

        direction = (target.position - core.transform.position);
        direction.y = 0;
        direction.Normalize();
        core.transform.forward = Vector3.RotateTowards(core.transform.forward, direction, turnSpeed * Time.deltaTime, 0);


        Debug.DrawRay(transform.position, Vector3.down * groundCheckLength, Color.red);
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckLength, groundLayer) && rb.velocity.y < -0.1f)
        {
            isComplete = true;
        }
        //Debug.Log("YVelo " + rb.velocity.y);
        float _time = Map(rb.velocity.y, v0 / 2, -v0 / 2, 0, 1, true);
        //Debug.Log("Time " + _time);
        animator.Play(animClip.name, 0, _time);

        
    }

    private float Map(float _value, float _min1, float _max1, float _min2, float _max2, bool _clamp = false)
    {
        float _val = _min2 + (_max2 - _min2) * ((_value - _min1) / (_max1 - _min1));
        return _clamp ? Mathf.Clamp(_val, Mathf.Min(_min2, _max2), Mathf.Max(_min2, _max2)) : _val;
    }

    public override void DoFixedUpdateState()
    {
        base.DoFixedUpdateState();
        rb.AddForce(Vector3.down * gravity, ForceMode.Force);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
        animator.StopPlayback();
        animator.speed = 1;
    }

}
