using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVaultSequence : StateSequence
{
    [SerializeField] private Player player;
    public override void DoEnterLogic()
    {
        player.playerObj.transform.forward = -player.wallSensor.wallHit.normal;
        player.stats.gravityEnabled = false;
        base.DoEnterLogic();
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        
        player.stats.gravityEnabled = true;
    }
}
