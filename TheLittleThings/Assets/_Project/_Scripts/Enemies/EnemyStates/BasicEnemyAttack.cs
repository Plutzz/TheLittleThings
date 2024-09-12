using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAttack : State
{
    [SerializeField] private State chargeUp, attack;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        stateMachine.SetState(chargeUp);
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();

    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();
        if(chargeUp.isComplete)
        {
            stateMachine.SetState(attack);
        }

        if (attack.isComplete)
        {
            isComplete = true;
        }
    }

}
