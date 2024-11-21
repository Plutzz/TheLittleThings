using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeupAttackState : State
{
    [SerializeField] private PlayerChooseAttack chooseAttack;
    [SerializeField] private float stopAnimationTime = 0.5f;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        rb.velocity = Vector2.zero;
        
        // Set animation to length of attack
        
        animator.StartPlayback();
        animator.speed = 0;
        

    }
    
    public override void DoUpdateState()
    {
        base.DoUpdateState();
        float _time = Map(stateUptime, 0, chooseAttack.minChargeupTime, 0, stopAnimationTime, true);
        animator.Play("Attack", 0, _time);
    }
    
    private float Map(float _value, float _min1, float _max1, float _min2, float _max2, bool _clamp = false)
    {
        float _val = _min2 + (_max2 - _min2) * ((_value - _min1) / (_max1 - _min1));
        return _clamp ? Mathf.Clamp(_val, Mathf.Min(_min2, _max2), Mathf.Max(_min2, _max2)) : _val;
    }
}
