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
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        if (xInput != 0 && stateMachine.currentState != airborne)
        {
            stateMachine.SetState(move);
        }

        if(xInput == 0 && stateMachine.currentState != airborne)
        {
            stateMachine.SetState(idle);
        }

        transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), 1, 1);
    }
}
