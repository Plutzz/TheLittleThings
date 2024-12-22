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
        isComplete = false;
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        if (stateUptime >= hurtDuration) {
            isComplete = true;
        }  
    }
}
