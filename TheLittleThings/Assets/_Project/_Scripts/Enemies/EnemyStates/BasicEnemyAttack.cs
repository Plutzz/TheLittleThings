using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAttack : State
{
    [SerializeField] private State navigate, attack;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        stateMachine.SetState(navigate);
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();

    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();
        if(navigate.isComplete)
        {
            stateMachine.SetState(attack);
        }

        if (attack.isComplete)
        {
            isComplete = true;
        }
    }

}
