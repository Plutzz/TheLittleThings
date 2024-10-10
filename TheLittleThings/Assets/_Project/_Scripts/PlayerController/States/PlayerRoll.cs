using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : State
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Player player;
    [SerializeField] private Transform orientation;
    private PlayerStats stats => player.stats;
    public AnimationClip rollAnimation;
    private float prevDrag;
    
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Roll();
        prevDrag = rb.drag;
        rb.drag = stats.RollDrag;
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

    private void Roll()
    {
        Vector3 rollDirFor = Vector3.ProjectOnPlane(orientation.forward, Vector3.up).normalized * playerInput.yInput;
        Vector3 rollDirRight = Vector3.ProjectOnPlane(orientation.right, Vector3.up).normalized * playerInput.xInput;
        Vector3 rollDir = (rollDirFor + rollDirRight).normalized;

        if (rollDir == Vector3.zero)
        {
            rollDir = Vector3.ProjectOnPlane(player.transform.forward, Vector3.up).normalized;
        }

        //animator.Play("Roll");
        rb.velocity = new Vector3(rollDir.x * stats.RollSpeed, rb.velocity.y, rollDir.z * stats.RollSpeed);
        
    }
}
