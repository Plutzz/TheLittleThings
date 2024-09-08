using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : State
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play("Idle");
    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();
    }
}
