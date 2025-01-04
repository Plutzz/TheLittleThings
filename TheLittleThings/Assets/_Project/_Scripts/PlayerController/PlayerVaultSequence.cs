using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVaultSequence : StateSequence
{
    [SerializeField] private Player player;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        player.stats.gravityEnabled = false;
        player.transform.forward = -player.wallSensor.wallHit.normal;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        player.stats.gravityEnabled = true;
    }
}
