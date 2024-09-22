using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBeast : StateMachineCore
{
    [SerializeField] private IdleState idle;
    [SerializeField] private EnemyChooseRandom attack;

    // Start is called before the first frame update
    void Start()
    {
        SetupInstances();
        stateMachine.SetState(idle);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.DoUpdateBranch();

        if (stateMachine.currentState.isComplete)
        {
            if (stateMachine.currentState == idle)
            {
                stateMachine.SetState(attack);
            }
            else if (stateMachine.currentState == attack)
            {
                stateMachine.SetState(idle);
            }
        }
    }
}
