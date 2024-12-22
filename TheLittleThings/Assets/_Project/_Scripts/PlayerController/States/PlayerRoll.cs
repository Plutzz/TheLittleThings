using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : State
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Player player;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerObj;
    private PlayerStats stats => player.stats;
    private float prevDrag;
    
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        
        prevDrag = rb.drag;
        rb.drag = stats.RollDrag;
        
        // Make player roll in the direction that they are pressing relative to the camaera
        Vector2 _inputVector = playerInput.moveVector;
        Vector3 _inputDir = orientation.forward * _inputVector.y + orientation.right * _inputVector.x;
        playerObj.transform.forward = _inputDir;
        
        Roll();
    }
    
    public override void DoExitLogic()
    {
        base.DoExitLogic();
        rb.drag = prevDrag;
    }
    
    public override void DoUpdateState()
    {
        base.DoUpdateState();
        if(stateUptime > stats.RollDuration || rb.velocity.magnitude < 1) isComplete = true;
    }

    /// <summary>
    /// Adds an impulse force in the direction that the player is pressing and set roll animation
    /// </summary>
    private void Roll()
    {
        Vector3 rollDirFor = Vector3.ProjectOnPlane(orientation.forward, Vector3.up).normalized * playerInput.moveVector.y;
        Vector3 rollDirRight = Vector3.ProjectOnPlane(orientation.right, Vector3.up).normalized * playerInput.moveVector.x;
        Vector3 rollDir = (rollDirFor + rollDirRight).normalized;

        if (playerInput.moveVector.y == 0 && playerInput.moveVector.x == 0)
        {
            rollDir = Vector3.ProjectOnPlane(playerObj.transform.forward, Vector3.up).normalized;
        }

        player.SetTrigger("Roll");
        rb.velocity = new Vector3(rollDir.x * stats.RollSpeed, rb.velocity.y, rollDir.z * stats.RollSpeed);
    }
}
