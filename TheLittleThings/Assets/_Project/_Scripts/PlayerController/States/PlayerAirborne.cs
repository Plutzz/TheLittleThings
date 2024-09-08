using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborne : State
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.Play("Fall");
    }
}
