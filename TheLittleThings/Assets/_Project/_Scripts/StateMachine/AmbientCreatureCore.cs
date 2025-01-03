using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientCreatureCore : StateMachineCore
{
    public Transform player;
    public float scatterDistance = 5f;
    public float wanderDuration = 3f;

    private ScatterState scatterState;
    private PatrolState patrolState;

    // Start is called before the first frame update
   private void Start()
    {
        SetupInstances();

        scatterState = new ScatterState(player, scatterDistnace);
        patrolState = new PatrolState(wanderDuration);

        stateMachine.SetState(patrolState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
