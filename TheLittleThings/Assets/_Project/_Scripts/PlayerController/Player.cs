using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachineCore
{
    [SerializeField] private PlayerIdle idle;
    [SerializeField] private PlayerMove move;
    [SerializeField] private PlayerAirborne airborne;
    [SerializeField] private GroundSensor groundSensor;

    [SerializeField] private PlayerInput playerInput;

    public float NoInputDampenForce = 0.2f;

    // Start is called before the first frame update
    void Awake()
    {
        SetupInstances();
        stateMachine.SetState(idle);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.DoUpdateBranch();
        float xInput = playerInput.xInput;

        if (!groundSensor.grounded)
        {
            stateMachine.SetState(airborne);
        }
        else if (xInput != 0)
        {
            stateMachine.SetState(move);
        }
        else if(xInput == 0)
        {
            stateMachine.SetState(idle);
        }

        if(xInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (xInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
    }
}
