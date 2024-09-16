using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : State
{
    [SerializeField] private PlayerStats playerStats;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play("Idle");
    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();
    }

    public override void DoUpdateState()
    {
    }
}
