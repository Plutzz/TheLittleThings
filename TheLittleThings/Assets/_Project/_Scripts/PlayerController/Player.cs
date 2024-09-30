using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachineCore
{
    [HorizontalLine(color: EColor.Gray)]
    [Header("States")]
    [SerializeField] private PlayerIdle idle;
    [SerializeField] private PlayerMove move;
    [SerializeField] private PlayerAirborne airborne;
    [SerializeField] private PlayerWall wall;
    [SerializeField] private PlayerCombat attack;
    [HorizontalLine(color: EColor.Gray)]
    [Header("Sensors")]
    [SerializeField] private GroundSensor groundSensor;
    [SerializeField] private WallSensor wallSensor;
    [SerializeField] private Transform graphics;
    [HorizontalLine(color: EColor.Gray)]
    [Header("Player Components")]
    [Expandable]
    [SerializeField] public PlayerStats stats;
    [SerializeField] private PlayerInput playerInput;
    [Header("Camera Follow")]
    [SerializeField] private GameObject cameraFollowGameObject;
    public HealthTracker playerHP;
    public bool isFacingRight = true;
    public Transform playerTransform;
    private CameraFollowObject cameraFollowObject;
    

    // Start is called before the first frame update
    void Awake()
    {
        SetupInstances();
        ResetPlayer();
        rb.gravityScale = stats.NormalGravity;
        playerHP.OnEntityKilled += PlayerHP_OnEntityKilled;
        
        if(cameraFollowGameObject != null)
            cameraFollowObject = cameraFollowGameObject.GetComponent<CameraFollowObject>();
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

        // transitions
        if (!groundSensor.grounded)
        {
            if (IsWallSensorTriggered(xInput))  // Adjust wall check for rotation
            {
                stateMachine.SetState(wall);
            }
            else
            {
                stateMachine.SetState(airborne);
            }

        }
        else if (xInput != 0 && (stateMachine.currentState != attack || stateMachine.currentState.isComplete))
        {
            stateMachine.SetState(move);
        }
        else if (xInput == 0 && (stateMachine.currentState != attack || stateMachine.currentState.isComplete))
        {
            stateMachine.SetState(idle);
        }
        if (xInput != 0 && StateCanTurn())
        {
            TurnCheck(xInput);
        }

        if (Input.GetMouseButtonDown(0) && groundSensor.grounded)
        {
            stateMachine.SetState(attack);
        }

    }

    /// <summary>
    /// Checks if the player is facing a wall based on their rotation and movement direction
    /// </summary>
    private bool IsWallSensorTriggered(float xInput)
    {
        // Use local space for checking which side the wall is on
        bool isWallOnLeft = wallSensor.wallLeft && xInput < 0;
        bool isWallOnRight = wallSensor.wallRight && xInput > 0;

        return isWallOnLeft || isWallOnRight;
    }

    /// <summary>
    /// Adjust the player's facing direction based on xInput and the current facing direction.
    /// </summary>
    private void TurnCheck(float xInput)
    {
        if (xInput < 0 && isFacingRight)
        {
            Turn();
        }
        else if (xInput > 0 && !isFacingRight)
        {
            Turn();
        }
    }

    /// <summary>
    /// Turns the player around by rotating the playerTransform instead of scaling.
    /// </summary>
    private void Turn()
    {
        float yRotation = isFacingRight ? 180f : 0f;
        Vector3 rotator = new Vector3(playerTransform.rotation.x, yRotation, playerTransform.rotation.z);
        playerTransform.rotation = Quaternion.Euler(rotator);
        isFacingRight = !isFacingRight;
        
        cameraFollowObject.CallTurn();
    }

    /// <summary>
    /// Only allow turning in these states to prevent turning during attacks, etc.
    /// </summary>
    private bool StateCanTurn()
    {
        return stateMachine.currentState == move || stateMachine.currentState == idle || stateMachine.currentState == airborne;
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