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
        //state is active for a set number of frames
        if (stateUptime >= hurtDuration) {
            isComplete = true;
        }
    }
}

//reference player script and use statemachine 
//In hurt state use the stateUptime variable to check for how long the player has been in the hurt state for