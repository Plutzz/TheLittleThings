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
    private void Update()
    {
        if (stateMachine.currentState.isComplete)
        {
            if (Vector3.Distance(transform.position, player.position) < scatterDistance)
            {
                stateMachine.SetState(scatterState);
            }
            else
            {
                stateMachine.SetState(patrolState);
            }
        }
        stateMachine.currentState.DoUpdateBranch();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.DoFixedUpdateBranch();
    }
}

public class ScatterState : State
{
    private Transform player;
    private float scatterDistance;
    private Vector3 moveDirection;

    public ScatterState(Transform player, float scatterDistance)
    {
        this.player = player;
        this.scatterDistance = scatterDistance;
    }

    public override void DoEnterLogic()
    {
        Vector3 directionAway = (core.transform.position - player.position).normalized;
        moveDirection = directionAway;
        rb.velocity = moveDirection * rb.velocity.magnitude;
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        if (Vector3.Distance(core.transform.position, player.position) >= scatterDistance)
        {
            isComplete = true;
        }
    }

    public override void DoFixedUpdateState()
    {
        rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
    }

}

public class PatrolState : State
{
    private float wanderDuration;
    private float wanderTimer;
    private Vector3 moveDirection;

    public PatrolState(float wanderDuration)
    {
        this.wanderDuration = wanderDuration;
    }

    public override void DoEnterLogic()
    {
        wanderTimer = 0f;
        PickRandomDirection();
    }

    public override void DoUpateState()
    {
        base.DoUpdateState;
        wanderTimer += Time.deltaTime;
        if (wanderTimer >= wanderDuration)
        {
            isComplete = true;
        }
    }

    public override void DoFixedUpdateState()
    {
        rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
    }

    private void PickRandomDirection()
    {
        float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
        moveDirection = new Vector3(Mathf.cos(randomAngle), 0, Mathf.Sin(randomAngle)).normalized;
    }
}
