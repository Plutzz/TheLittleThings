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
        rb.drag = 100;
        lastPos = player.rb.position;
    }
    public override void DoUpdateState()
    {
        // player.rb.position = lastPos;
        // lastPos = player.rb.position;
    }
}
