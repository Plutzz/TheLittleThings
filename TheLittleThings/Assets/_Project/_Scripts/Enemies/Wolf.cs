using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : StateMachineCore
{
    [SerializeField] private PatrolState patrol;
    [SerializeField] private EnemyAttack attack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.DoUpdateBranch();
    }
}
