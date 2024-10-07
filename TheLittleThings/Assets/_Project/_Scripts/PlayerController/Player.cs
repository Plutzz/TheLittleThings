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
    [SerializeField] private PlayerRoll roll;
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
    public HealthTracker playerHP;
    [Header("Camera")] 
    [SerializeField] private CameraFollowObject cameraFollowObject;
    public bool isFacingRight = true;


    // Start is called before the first frame update
    void Awake()
    {
        SetupInstances();
        ResetPlayer();
        rb.useGravity = false;
        ChangeGravity(stats.NormalGravity);
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
            if ((wallSensor.wallLeft && xInput < 0) || (wallSensor.wallRight && xInput > 0))
            {
                stateMachine.SetState(wall);
            }
            else if (stateMachine.currentState != roll)
            {
                stateMachine.SetState(airborne);
            }

        }
        else if (xInput != 0 && ((stateMachine.currentState != roll && stateMachine.currentState != attack) || stateMachine.currentState.isComplete))
        {
            stateMachine.SetState(move);
        }
        else if (xInput == 0 && ((stateMachine.currentState != roll && stateMachine.currentState != attack) || stateMachine.currentState.isComplete))
        {
            stateMachine.SetState(idle);
        }
        if (xInput != 0 && (stateMachine.currentState == move || stateMachine.currentState == idle || stateMachine.currentState == airborne))
        {
            TurnCheck(xInput);
        }

        if (Input.GetMouseButtonDown(0) && groundSensor.grounded)
        {
            stateMachine.SetState(attack);
        }
        
        if (playerInput.ctrlPressedThisFrame)
        {
            stateMachine.SetState(roll);
        }
    }

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
        Vector3 rotator = new Vector3(transform.rotation.x, yRotation, transform.rotation.z);
        transform.rotation = Quaternion.Euler(rotator);
        isFacingRight = !isFacingRight;
        
        cameraFollowObject?.CallTurn();
        RotateSensors();
    }
    /**
     * Rotates the wall sensors so they don't get flipped when the player turns around.
     */
    private void RotateSensors()
    {
        wallSensor.transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
    }
    
    private Vector2 GetDirection()
    {
        return isFacingRight ? Vector2.right : Vector2.left;
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

    public void ChangeGravity(float _gravity)
    {
        stats.CurrentGravity = _gravity;
    }

    void FixedUpdate() 
    {
        Debug.Log("Gravity");
        rb.AddForce(Vector3.down * stats.CurrentGravity, ForceMode.Force);
        stateMachine.currentState.DoFixedUpdateBranch(); 
    }
}