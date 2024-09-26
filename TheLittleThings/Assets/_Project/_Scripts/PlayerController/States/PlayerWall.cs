using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWall : State
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Player player;
    private PlayerStats stats => player.stats;
    [SerializeField] private WallSensor wallSensor;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play("Wall Slide");
        rb.gravityScale = stats.WallGravity;
        rb.velocity = Vector2.right * rb.velocity.x;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        rb.gravityScale = stats.NormalGravity;
    }

    public override void DoFixedUpdateState()
    {
        //if velocity is less than maxspeed
        if (Mathf.Abs(rb.velocity.x) < stats.MaxSpeed)
        {
            //movement on wall if the wall is on the right, doesn't let player add force against wall because then they will stop moving since the wall has a friction of 0.4
            if (wallSensor.wallRight)
            {
                rb.AddForce(Vector2.right * Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -1, 0) * stats.Acceleration);
            }
            //movement on the wall if the wall is on the left
            else if (wallSensor.wallLeft)
            {
                rb.AddForce(Vector2.right * Mathf.Clamp(Input.GetAxisRaw("Horizontal"), 0, 1) * stats.Acceleration);
            }
        }
    }
}
