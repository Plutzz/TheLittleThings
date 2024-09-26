using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PatrolState : State
{
    [SerializeField] private IdleState idleState;
    [SerializeField] private NavigateState navigateState;
    private Transform anchor1, anchor2;
    [SerializeField] private float patrolTime = 2f;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //GoToNextDestination();
        //stateMachine.SetState(idleState);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Debug.Log("awagga");
        navigateState.destination = () => collision.gameObject.transform.position;
        stateMachine.SetState(navigateState);
    }
    public override void CheckTransitions()
    {
        base.CheckTransitions();

        if (stateUptime > patrolTime)
        {
            isComplete = true;
            return;
        }

        //if (!currentState.isComplete) return;
        //if(currentState == idleState)
        //{
        //    GoToNextDestination();
        //}
        //else
        //{
        //    stateMachine.SetState(idleState);
        //}

    }


}
