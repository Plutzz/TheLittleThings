using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldAttack : State
{
    // [SerializeField] private float maxHoldTimer = 2f;

    // int minDamage
    // int maxDamage
    // int currentDamage

    // Create 2 child states: HoldChargeup and HoldDoAttack
    // To test, put debug statements
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("HoldAttack");
        // Transition to HoldChargeup
        // currentDamage = minDamage
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();

        // ramp up damage with time
        // Lerp currentDamage between minDamage and maxDamage to calculate scaling damage


        //if(mouse up || stateUptime > maxHoldTimer)
            //Transition to HoldDoAttack


        // When HoldDoAttack isComplete == true, mark this state as complete

    }
}
