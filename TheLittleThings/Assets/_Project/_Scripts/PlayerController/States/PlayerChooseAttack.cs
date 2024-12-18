using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerChooseAttack : State
{
    [SerializeField] public PlayerComboAttack comboAttack;
    [SerializeField] private PlayerHoldAttack holdAttack;
    [SerializeField] public float minChargeupTime, maxChargeupTime;
    [SerializeField] private ChargeupAttackState chargeupAttackState;
    [SerializeField] private PlayerInput playerInput;

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

        // When attack is complete, mark this state as complete
        if (currentState.isComplete)
        {
            isComplete = true;
        }
        
        if (currentState != chargeupAttackState) return;
        
        // If the player has been charging up the attack for long, force the player to do a holdAttack even if they haven't let go of attack
        if (maxChargeupTime < stateUptime)
        {
            stateMachine.SetState(holdAttack);
        }
        
        // Player releases attack button
        if (!playerInput.attackHeld)
        {
            // If the player has not held attack for long enough to perform a hold attack, perform the next combo attack
            if (minChargeupTime>stateUptime)
            {
                stateMachine.SetState(comboAttack);
            }
            // If the player has performed a hold attack for long enough, commit to the holdAttack
            else
            {
                stateMachine.SetState(holdAttack);
            }
        }
        
        
    }
    
}
