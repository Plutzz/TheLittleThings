using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigateJump : State
{
    [SerializeField] private AnimationClip animClip;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckLength = 1f;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        //animator.Play(animClip.name);
    }
    public void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * groundCheckLength, Color.red);
    }
    public override void DoUpdateState()
    {
        base.DoUpdateState();
        Debug.DrawRay(transform.position, Vector3.down * groundCheckLength, Color.red);
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckLength, groundLayer) && rb.velocity.y < -0.1f)
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
