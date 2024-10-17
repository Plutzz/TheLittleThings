using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SocialPlatforms.Impl;
public class PlayerAttack : State
{
    [SerializeField] private float attackTime = 1f;
    [SerializeField] private float attackMoveAmount = 0.5f;
    [SerializeField] private float finalAttackMoveAmount = 50f;
    [SerializeField] private float groundDrag = 2f;
    [SerializeField] private Transform playerObj;
    private float timer;
    [SerializeField] private PlayerAttackManager attackManager;
    [SerializeField] private PlayerInput inputManager;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        isComplete = false;
        timer = attackTime;
        attackManager.AttackPoint.SetActive(true);
        rb.drag = groundDrag;
        rb.velocity = Vector2.zero;
        if (attackManager.FinalAttack)
        {
            rb.AddForce(playerObj.forward * finalAttackMoveAmount, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(playerObj.forward * attackMoveAmount, ForceMode.Impulse);
        }

    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        attackManager.AttackPoint.SetActive(false);
        rb.drag = 0;
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
    }

    public void SetIsComplete(bool _isComplete)
    {
        isComplete = _isComplete;
    }
}