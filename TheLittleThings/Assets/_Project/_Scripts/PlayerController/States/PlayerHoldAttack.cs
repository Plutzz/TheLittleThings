using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldAttack : State
{

    [SerializeField] private float startAnimationTime = 0.1f;
    [SerializeField] private float attackSpeed = 1f;
    // int minDamage
    // int maxDamage
    // int currentDamage
    
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        animator.StartPlayback();
        animator.speed = 0;
        animator.Play("Attack", 0, startAnimationTime);
    }

    // public override void DoUpdateState()
    // {
    //     base.DoUpdateState();
    //
    //     // ramp up damage with time
    //     // Lerp currentDamage between minDamage and maxDamage to calculate scaling damage
    //
    //
    //     //if(mouse up || stateUptime > maxHoldTimer)
    //         //Transition to HoldDoAttack
    //
    //
    //     // When HoldDoAttack isComplete == true, mark this state as complete
    //     
    // }
    public override void DoUpdateState()
    {
        base.DoUpdateState();
        float _time = Map(stateUptime, 0, attackSpeed, startAnimationTime, 1, true);
        animator.Play("Attack", 0, _time);
        if (_time > 0.95f)
        {
            isComplete = true;
        }
    }
    
    private float Map(float _value, float _min1, float _max1, float _min2, float _max2, bool _clamp = false)
    {
        float _val = _min2 + (_max2 - _min2) * ((_value - _min1) / (_max1 - _min1));
        return _clamp ? Mathf.Clamp(_val, Mathf.Min(_min2, _max2), Mathf.Max(_min2, _max2)) : _val;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        animator.speed = 1;
        animator.StopPlayback();
    }
}
