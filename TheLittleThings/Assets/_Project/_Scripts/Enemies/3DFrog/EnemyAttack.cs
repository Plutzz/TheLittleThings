using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : State
{
    [SerializeField] private State navigate, chargeUp, attack;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        stateMachine.SetState(navigate);
    }
    public override void CheckTransitions()
    {
        base.CheckTransitions();
        if (!currentState.isComplete) return;
        if (currentState == navigate)
        {
            stateMachine.SetState(chargeUp);
        }
        else if (currentState == chargeUp)
        {
            stateMachine.SetState(attack);
        }
        else if (currentState == attack)
        {
            isComplete = true;
        }
    }
}
