using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : State
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Player player;
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
        if(stateUptime > stats.RollDuration)
        {
            isComplete = true;
        }
    }

    private void Roll()
    {
        animator.Play("Roll");
        rb.velocity = new Vector2(playerInput.xInput * stats.RollSpeed, rb.velocity.y);
    }
}
