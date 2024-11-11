using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : State
{
    [SerializeField] private AnimationClip wakeUpAnimClip;
    [SerializeField] private State wokenUpState;

    private float wokenUpAt = float.MaxValue;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        rb.velocity = Vector3.zero;

    }
    
    public override void DoUpdateState()
    {
        if (stateUptime - wokenUpAt > wakeUpAnimClip.length)
        {
            isComplete = true;
            
        }
        base.DoUpdateState();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (wokenUpAt == float.MaxValue)
        {
            animator.Play(wakeUpAnimClip?.name);
            wokenUpAt = stateUptime;
        }
        
    }
}
