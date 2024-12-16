using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : State
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //player gets hit by enemy hitbox
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        //state is active for a set number of frames
    }
}
