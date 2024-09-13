using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DavidPlayerMove : State
{
    public float acceleration = 20f;
    public float maxSpeed = 10f;
    public float jumpForce = 1f;

    private bool jumpReady = true;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play("Run");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoUpdateState()
    {
        if(jumpReady == true && Input.GetKeyDown(KeyCode.Space))
        {
            jumpReady = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            jumpReady = true;
        }
        
    }
    public override void DoFixedUpdateState()
    {
        base.DoFixedUpdateState();
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(Vector2.right * Input.GetAxisRaw("Horizontal") * acceleration);
        }
        else
        {
            rb.velocity = (new Vector2(Input.GetAxisRaw("Horizontal") * maxSpeed, rb.velocity.y));
        }
    }

}