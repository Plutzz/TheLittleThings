using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeupState : State
{
    [SerializeField] private AnimationClip wakeUpAnimation;
    [SerializeField] private GameObject cutsceneCamera;


    public override void DoEnterLogic()
    {
        // Start animation
        base.DoEnterLogic();
        animator.Play(wakeUpAnimation.name);
        cutsceneCamera.SetActive(true);
        CutsceneManager.Instance?.cinematicBars.ActivateBars();
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        if (stateUptime > wakeUpAnimation.length)
        {
            isComplete = true;
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        cutsceneCamera.SetActive(false);
        CutsceneManager.Instance?.cinematicBars.DeactivateBars();
    }
    
}
