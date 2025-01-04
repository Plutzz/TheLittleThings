using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player rises up the wall until the designated point
/// </summary>
public class PlayerVaultRise : State
{
    [SerializeField] private Player player;
    [SerializeField] private WallSensor vaultStopSensor;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        rb.velocity = Vector3.zero;
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        rb.velocity = Vector3.up * player.stats.ClimbingRiseSpeed;
        if (!vaultStopSensor.OnWall)
        {
            isComplete = true;
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        rb.velocity = Vector3.zero;
    }

}
