using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : State
{
    [SerializeField] private Player player;
    private PlayerStats stats => player.stats;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.SetTrigger("Idle");
        //animator.Play("Idle");
        rb.drag = 4;
    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();
    }

    public override void DoUpdateState()
    {
    }
}
