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
    [field: SerializeField] public PlayerAirborne3D airborne { get; private set; }
    [field: SerializeField] public PlayerChooseAttack attack { get; private set; }
    
    // Sensor scripts used for ground checks and wall checks
    [HorizontalLine(color: EColor.Gray)]
    [Header("Sensors")]
    [SerializeField] private GroundSensor groundSensor;
    
    // References to other components on the player
    [HorizontalLine(color: EColor.Gray)]
    [Header("Player Components")]
    [SerializeField] private Transform graphics;
    [Expandable]
    [SerializeField] public PlayerStats stats;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] public HealthTracker playerHP {get; private set;}
    
    // Variables pertaining to player camera
    [Header("Camera")] 
    [SerializeField] private CameraFollowObject cameraFollowObject;
    public bool isFacingRight = true;

    
    // Variables pertaining to player attacks
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
        // Cache xInput and yInput from playerInput script
        float xInput = playerInput.xInput;
        float yInput = playerInput.yInput;
        
        // Transition to roll
        if (stateMachine.currentState != attack && playerInput.ctrlPressedThisFrame)
        {
            stateMachine.SetState(roll);
            return;
        }
        
        // Transition to airborne
        if (!groundSensor.grounded && stateMachine.currentState != roll)
        {
            stateMachine.SetState(airborne);
            return;
        }
        
        // Transition to move
        if ((xInput != 0 || yInput != 0) && ((stateMachine.currentState != roll && stateMachine.currentState != attack) || stateMachine.currentState.isComplete))
        {
            stateMachine.SetState(move);
            return;
        }
        
        // Transition to idle
        if ((xInput == 0 || yInput == 0) && ((stateMachine.currentState != roll && stateMachine.currentState != attack) || stateMachine.currentState.isComplete))
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
            UnityEditor.Handles.Label(transform.position + Vector3.up * 2.25f, rb.velocity.ToString(), style);
        
        }
#endif
    }
    #endregion
    
}