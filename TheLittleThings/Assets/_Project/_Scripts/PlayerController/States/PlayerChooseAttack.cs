using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChooseAttack : State
{
    [SerializeField] private float holdTime = 0.25f;
    [SerializeField] public PlayerAttack comboAttack;
    [SerializeField] private PlayerCombat holdAttack;
    [SerializeField] private DummyState dummyState;

    public override void DoEnterLogic()
    {
        Debug.Log("Hello");
        base.DoEnterLogic();
        stateMachine.SetState(dummyState);
    }

    public override void DoUpdateState()
    {
      
        base.DoUpdateState();

        if (currentState.isComplete)
        {
            isComplete = true;
        }

        if (currentState != dummyState) return;
        
        if (Input.GetMouseButtonUp(0))
        {
            //this code is ran the first time the mouse button is up
            if (holdTime>stateUptime)
            {
                stateMachine.SetState(comboAttack);
                //transision to combo attack
            }
        }
        if (holdTime < stateUptime)
        {
            stateMachine.SetState(holdAttack);
            //tansition to hold attack
        }

    }
    
}
