using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : State
{
    [SerializeField] private float hurtDuration = 0.5f;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        if (stateUptime >= hurtDuration) {
            isComplete = true;
        }
    }
}
