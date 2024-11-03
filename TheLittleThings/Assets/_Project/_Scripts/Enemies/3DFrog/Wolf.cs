using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Wolf : StateMachineCore
{
    [SerializeField] private State idleState;
    [SerializeField] private EnemyChooseRandom attack;

    // Start is called before the first frame update
    void Start()
    {
        SetupInstances();
        stateMachine.SetState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.DoUpdateBranch();

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
        }
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.TryGetComponent(out HealthTracker other))
    //    {
    //        other.DamageEntity(10f, "Wolf", 5);
    //    }
    //}
}
