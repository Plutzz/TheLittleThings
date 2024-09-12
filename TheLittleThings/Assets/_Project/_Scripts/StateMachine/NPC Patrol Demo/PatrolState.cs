using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private IdleState idleState;
    [SerializeField] private NavigateState navigateState;
    [SerializeField] private Transform anchor1, anchor2;
    [SerializeField] private float patrolTime = 2f;

    private void GoToNextDestination()
    {
        float randomSpot = Random.Range(anchor1.position.x, anchor2.position.x);
        navigateState.destination = new Vector2(randomSpot, core.transform.position.y);
        stateMachine.SetState(navigateState);
    }


    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        GoToNextDestination();
        stateMachine.SetState(idleState);
    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();

        if (stateUptime > patrolTime)
        {
            isComplete = true;
            return;
        }

        if (!currentState.isComplete) return;
        if(currentState == idleState)
        {
            GoToNextDestination();
        }
        else
        {
            stateMachine.SetState(idleState);
        }

    }


}
