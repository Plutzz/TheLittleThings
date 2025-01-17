using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachineCore
{
    
    // Variables that hold what states the player can be in
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header("States")]
    [field: SerializeField] public PlayerIdle idle { get; private set; }
    [field: SerializeField] public PlayerMove3D move { get; private set; }
    [field: SerializeField] public PlayerRoll roll { get; private set; }
    [field: SerializeField] public State vault { get; private set; }
    [field: SerializeField] public PlayerAirborne3D airborne { get; private set; }
    [field: SerializeField] public PlayerChooseAttack attack { get; private set; }
    [field: SerializeField] public PlayerHurt hurt { get; private set; }
    
    // Sensor scripts used for ground checks and wall checks
    [field:HorizontalLine(color: EColor.Gray)]
    [field:Header("Sensors")]
    [field:SerializeField] public GroundSensor groundSensor {get; private set;}
    [field:SerializeField] public WallSensor wallSensor {get; private set;}
    [field:SerializeField] public SlopeSensor slopeSensor {get; private set;}

    
    // References to other components on the player
    [HorizontalLine(color: EColor.Gray)]
    [Header("Player Components")]
    [SerializeField] private Transform graphics;
    [field:SerializeField] public Transform playerObj { get; private set; }
    [Expandable]
    [SerializeField] public PlayerStats stats;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerJumpManager jumpManager;
    [SerializeField] public HealthTracker playerHP {get; private set;} 
    
    // Variables pertaining to atacks
    [HorizontalLine(color: EColor.Gray)]
    [Header("Attacks")]
    [SerializeField] private PlayerAttackManager attackManager;
    
    // Variables used for debugging
    [Header("Debug")] 
    [SerializeField] private Vector3 spawnPos;

    #region Unity Methods
    void Awake()
    {
        SetupInstances();
        ResetPlayer();
        
        // Disable gravity and simulate gravity manually (to allow for different gravity scales)
        rb.useGravity = false;
        stats.gravityEnabled = true;
        ChangeGravity(stats.NormalGravity);
        
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        // Calls update logic in the currently active state
        stateMachine.currentState.DoUpdateBranch();
        
        // Debug reset player input
        if (playerInput.ResetInput)
        {
            ResetPlayer();
        }


        // State transitions
        HandleTransitions();
        
    }
    
    void FixedUpdate() 
    {
        // Simulate custom gravity
        rb.AddForce(Vector3.down * stats.CurrentGravity, ForceMode.Force);
        
        // Call FixedUpdate logic
        stateMachine.currentState.DoFixedUpdateBranch(); 
    }

    #endregion
    
    #region Helper (Private) Methods
    /// <summary>
    /// Handles transitions between states in player state machine
    /// </summary>
    private void HandleTransitions()
    {
        // If the player is currently in hurt state and the state isn't done yet, ignore all transitions
        // Makes it so that the player can't get out of the hurt state early
        if (stateMachine.currentState == hurt && !stateMachine.currentState.isComplete) return;
        
        // Cache xInput and yInput from playerInput script
        float xInput = playerInput.moveVector.x;
        float yInput = playerInput.moveVector.y;
        
        // Transition to roll
        if (stateMachine.currentState != attack && stateMachine.currentState != airborne && playerInput.rollPressedThisFrame)
        {
            stateMachine.SetState(roll);
            return;
        }
        
        // Transition to vault when vault cooldown is off and player is either on the ground or in the air
        if (wallSensor.OnWall && ((stateMachine.currentState == move || stateMachine.currentState == idle) || stateMachine.currentState == airborne))
        {
            stateMachine.SetState(vault);
        }
            
        // Transition to airborne
        if (!groundSensor.grounded && !slopeSensor.isOnSlope && stateMachine.currentState != roll && stateMachine.currentState != vault)
        {
            stateMachine.SetState(airborne);
            return;
        }
        
        // condition for transitioning to a "grounded" state (move or idle) when transitioning from airborne
        bool airborneGroundCheck = stateMachine.currentState == airborne && rb.velocity.y <= 0;
        
        // condition for transitioning to a "grounded" state (move or idle) when transitioning from any state besides airborne
        bool nonAirborneGroundCheck = stateMachine.currentState == idle || stateMachine.currentState == move;
        
        // Transition to move
        if (groundSensor.grounded && (xInput != 0 || yInput != 0) && (nonAirborneGroundCheck || airborneGroundCheck || stateMachine.currentState.isComplete))
        {
            stateMachine.SetState(move);
            return;
        }

        float timeSinceLastMove = Time.time - playerInput.timeOfLastMoveInput;
        
        // Transition to idle
        if (groundSensor.grounded && timeSinceLastMove >= 0.1f && (nonAirborneGroundCheck || airborneGroundCheck || stateMachine.currentState.isComplete))
        {
            stateMachine.SetState(idle);
            return;
        }
    }
    
    #endregion
    
    #region Public Methods
    
    /// <summary>
    /// Changes the custom gravity scale that the player is currently experiencing
    /// </summary>
    /// <param name="gravity"></param>
    public void ChangeGravity(float gravity)
    {
        stats.CurrentGravity = gravity;
    }

    /// <summary>
    /// Reset all animation triggers. If a new trigger is added to the animator, it needs to be reset in this function.
    /// </summary>
    public void ResetAllTriggers()
    {
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Walk");
        animator.ResetTrigger("Roll");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Idle");
    }

    /// <summary>
    /// Set a trigger in the player's animator
    /// </summary>
    /// <param name="trigger"></param>
    public void SetTrigger(string trigger)
    {
        ResetAllTriggers();
        animator.SetTrigger(trigger);
    }

    #endregion
    
    #region Debug Methods
    /// <summary>
    /// DEBUG METHOD. Resets the player's position and health
    /// </summary>
    private void ResetPlayer()
    {
        stateMachine.SetState(idle);
        rb.velocity = Vector3.zero;
        rb.transform.position = spawnPos;

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
            Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            UnityEditor.Handles.Label(transform.position + Vector3.up * 2.25f, rb.velocity + "\n" + flatVel.magnitude, style);
        
        }
#endif
    }
    #endregion
    
}