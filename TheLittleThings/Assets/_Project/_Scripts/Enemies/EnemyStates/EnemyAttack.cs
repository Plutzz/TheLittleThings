using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : State
{
    [SerializeField] private List<State> enemyAttackStates;
    private State state;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        state = enemyAttackStates[Random.Range(0, enemyAttackStates.Count)];
        stateMachine.SetState(state, true);
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        if (state.isComplete)
        {
            isComplete = true;
        }
    }
}
