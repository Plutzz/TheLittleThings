using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSensor : MonoBehaviour
{
    public BoxCollider2D wallLeftCheck, wallRightCheck;
    public LayerMask groundLayer;
    public bool wallLeft { get; private set; }
    public bool wallRight { get; private set; }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        wallLeft = Physics2D.OverlapAreaAll(wallLeftCheck.bounds.min, wallLeftCheck.bounds.max, groundLayer).Length > 0;
        wallRight = Physics2D.OverlapAreaAll(wallRightCheck.bounds.min, wallRightCheck.bounds.max, groundLayer).Length > 0;
    }
}
