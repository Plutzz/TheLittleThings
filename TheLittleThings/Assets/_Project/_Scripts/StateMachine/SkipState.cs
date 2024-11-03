using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipState : State
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        isComplete = true;
    }
}
