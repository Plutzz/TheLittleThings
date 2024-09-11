using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachineCore
{
    [SerializeField] private PlayerIdle idle;
    [SerializeField] private PlayerMove move;
    [SerializeField] private PlayerAirborne airborne;

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
        float xInput = Input.GetAxisRaw("Horizontal");

        if (xInput != 0 && stateMachine.currentState != airborne)
        {
            stateMachine.SetState(move);
        }

        if(xInput == 0 && stateMachine.currentState != airborne)
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
