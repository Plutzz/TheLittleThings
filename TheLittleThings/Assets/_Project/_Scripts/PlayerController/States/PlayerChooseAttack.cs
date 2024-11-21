using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerChooseAttack : State
{
    [SerializeField] public PlayerComboAttack comboAttack;
    [SerializeField] private PlayerHoldAttack holdAttack;
    [SerializeField] public float minChargeupTime, maxChargeupTime;
    [FormerlySerializedAs("dummyState")] [SerializeField] private ChargeupAttackState chargeupAttackState;

    public override void DoEnterLogic()
    {
        Debug.Log("Hello");
        base.DoEnterLogic();
        stateMachine.SetState(chargeupAttackState);
        rb.velocity = Vector3.zero;
    }

    public override void DoUpdateState()
    {
      
        base.DoUpdateState();

        if (currentState.isComplete)
        {
            isComplete = true;
        }

        if (currentState != chargeupAttackState) return;
        
        if (!Input.GetMouseButton(0))
        {
            //this code is ran the first time the mouse button is up
            if (minChargeupTime>stateUptime)
            {
                stateMachine.SetState(comboAttack);
            }
            else
            {
                stateMachine.SetState(holdAttack);
            }
        }
        if (maxChargeupTime < stateUptime)
        {
            stateMachine.SetState(holdAttack);
        }
    }
    
}
