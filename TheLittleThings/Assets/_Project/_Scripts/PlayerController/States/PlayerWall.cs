using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWall : State
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private WallSensor wallSensor;
    [SerializeField] private float gravityOnWall = 0.5f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 50f;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play("Wall Slide");
        rb.gravityScale = gravityOnWall;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        rb.gravityScale = 1;
    }

    public override void DoFixedUpdateState()
    {
        //if velocity is less than maxspeed
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            //movement on wall if the wall is on the right, doesn't let player add force against wall because then they will stop moving since the wall has a friction of 0.4
            if (wallSensor.wallRight)
            {
                rb.AddForce(Vector2.right * Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -1, 0) * acceleration);
            }
            //movement on the wall if the wall is on the left
            else if (wallSensor.wallLeft)
            {
                rb.AddForce(Vector2.right * Mathf.Clamp(Input.GetAxisRaw("Horizontal"), 0, 1) * acceleration);
            }
        }
    }
}
