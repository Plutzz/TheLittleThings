using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Wolf : StateMachineCore
{
    [SerializeField] private PatrolState patrol;
    [SerializeField] private EnemyAttack attack;

    // Start is called before the first frame update
    void Start()
    {
        SetupInstances();
        stateMachine.SetState(patrol);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.DoUpdateBranch();

        if(stateMachine.currentState.isComplete)
        {
            if (stateMachine.currentState == patrol)
            {
                stateMachine.SetState(attack);
            }
            else if(stateMachine.currentState == attack)
            {
                stateMachine.SetState(patrol);
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
