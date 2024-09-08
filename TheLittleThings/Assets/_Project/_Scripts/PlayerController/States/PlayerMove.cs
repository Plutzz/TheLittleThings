using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : State
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play("Run");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
}
