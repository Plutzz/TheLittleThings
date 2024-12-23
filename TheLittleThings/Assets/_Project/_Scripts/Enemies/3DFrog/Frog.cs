using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

public class Frog : StateMachineCore
{
    [SerializeField] private State idleState, sleepState, wakeUpState;
    [SerializeField] private EnemyChooseRandom attack;

    // Start is called before the first frame update
    void Start()
    {
        SetupInstances();
        stateMachine.SetState(sleepState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.DoUpdateBranch();
        HandleTransitions();
    }

    void FixedUpdate()
    {
        stateMachine.currentState.DoFixedUpdateBranch();
    }
    
    /// <summary>
    /// Handles transitions between top level states
    /// </summary>
    private void HandleTransitions()
    {
        if(stateMachine.currentState.isComplete)
        {
            if (stateMachine.currentState == idleState)
            {
                stateMachine.SetState(attack);
            }
            else if(stateMachine.currentState == attack)
            {
                stateMachine.SetState(idleState);
            }
            else if (stateMachine.currentState == sleepState)
            {
                stateMachine.SetState(wakeUpState);
            }
            else if (stateMachine.currentState == wakeUpState)
            {
                stateMachine.SetState(idleState);
            }
        }
    }
}
