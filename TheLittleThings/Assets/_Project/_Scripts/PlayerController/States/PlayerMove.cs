using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : State
{
    private float speed = 5;
    [SerializeField] private PlayerInput playerInput;
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
        rb.velocity = Vector2.right * playerInput.xInput * speed;
    }

}
