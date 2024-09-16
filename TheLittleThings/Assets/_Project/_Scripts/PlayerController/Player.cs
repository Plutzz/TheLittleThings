using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachineCore
{
    [SerializeField] private PlayerIdle idle;
    [SerializeField] private PlayerMove move;
    [SerializeField] private PlayerAirborne airborne;
    [SerializeField] private GroundSensor groundSensor;

    public HealthTracker playerHP;
    [SerializeField] private PlayerInput playerInput;

    public float NoInputDampenForce = 0.2f;

    // Start is called before the first frame update
    void Awake()
    {
        SetupInstances();
        ResetPlayer();

        playerHP.OnEntityKilled += PlayerHP_OnEntityKilled;
    }

    private void PlayerHP_OnEntityKilled(float damage, string source, int iframes)
    {
        ResetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.DoUpdateBranch();
        float xInput = playerInput.xInput;

        if (playerInput.ResetInput)
        {
            ResetPlayer();
        }

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

    private void ResetPlayer()
    {
        stateMachine.SetState(idle);
        rb.velocity = Vector3.zero;
        rb.transform.position = new Vector3(0, -3, 0);

        playerHP?.ResetHP();
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;
            UnityEditor.Handles.Label(transform.position + Vector3.up * 2.25f, rb.velocity.ToString(), style);

        }
#endif
    }

    void FixedUpdate() { stateMachine.currentState.DoFixedUpdateBranch(); }
}