using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: add damage scaling with how long player is holding attack as well as a longer hit stop.
public class PlayerHoldAttack : State
{
    [SerializeField] private float startAnimationTime = 0.1f;
    [SerializeField] private float attackSpeed = 1f;
    // int minDamage
    // int maxDamage
    // int currentDamage
    
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.StartPlayback();
        animator.speed = 0;
        animator.Play("Attack", 0, startAnimationTime);
    }

    // public override void DoUpdateState()
    // {
    //     base.DoUpdateState();
    //
    //     // ramp up damage with time
    //     // Lerp currentDamage between minDamage and maxDamage to calculate scaling damage
    //
    //
    //     //if(mouse up || stateUptime > maxHoldTimer)
    //         //Transition to HoldDoAttack
    //
    //
    //     // When HoldDoAttack isComplete == true, mark this state as complete
    //     
    // }
    public override void DoUpdateState()
    {
        base.DoUpdateState();
        float _time = Utilites.Map(stateUptime, 0, attackSpeed, startAnimationTime, 1, true);
        animator.Play("Attack", 0, _time);
        if (_time > 0.95f)
        {
            isComplete = true;
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        animator.speed = 1;
        animator.StopPlayback();
    }
}
