using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : State
{
    [SerializeField] private Player player;
    private Vector3 lastPos;
    private PlayerStats stats => player.stats;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        player.SetTrigger("Idle");
        player.SetTrigger("Idle");
        //animator.Play("Idle");
        rb.drag = 24;
        lastPos = player.rb.position;
    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();
    }

    public override void DoUpdateState()
    {
        player.rb.position = lastPos;
        lastPos = player.rb.position;
    }
}
