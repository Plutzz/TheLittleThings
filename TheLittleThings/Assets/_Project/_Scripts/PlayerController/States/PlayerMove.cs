using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : State
{
    private float speed = 5;
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
        float xinput = Input.GetAxis("Horizontal");
        rb.velocity = Vector2.right * xinput * speed;
    }

}
