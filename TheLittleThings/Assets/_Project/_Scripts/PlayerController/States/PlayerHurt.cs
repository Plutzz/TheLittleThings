using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : State
{
    public GameObject enemy;
    public Collider eCollider;
    public override void DoEnterLogic()
    {
        eCollider = enemy.GetComponent<Collider>();
        //player gets hit by enemy hitbox
        if (eCollider.bounds.Contains(transform.position)){
            Debug.Log("test");
            base.DoEnterLogic();
        }
    }

    public override void DoExitLogic()
    {
        //state is active for a set number of frames
        if (stateUptime >= 0.5f) {
            base.DoExitLogic();
        }
    }
}

//reference player script and use statemachine 
//In hurt state use the stateUptime variable to check for how long the player has been in the hurt state for