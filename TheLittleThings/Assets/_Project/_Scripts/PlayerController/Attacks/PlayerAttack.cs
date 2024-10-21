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
    [SerializeField] private Transform orientation;
    [SerializeField] private Player player;
    [SerializeField] private PlayerInput playerInput;
    private PlayerStats stats => player.stats;
    private float timer;
    [SerializeField] private PlayerAttackManager attackManager;
    [SerializeField] private PlayerInput inputManager;
    [HideInInspector] public float timeBeforeHitboxActive;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        isComplete = false;
        timer = attackTime;
        rb.drag = groundDrag;
        rb.velocity = Vector2.zero;
        Vector2 _inputVector = new Vector2(playerInput.xInput, playerInput.yInput);
        Vector3 _inputDir = orientation.forward * _inputVector.y + orientation.right * _inputVector.x;
        playerObj.transform.forward = _inputDir;
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
        if (stateUptime > timeBeforeHitboxActive)
        {
            attackManager.AttackPoint.SetActive(true);
        }
    }

    public void SetIsComplete(bool _isComplete)
    {
        isComplete = _isComplete;
    }
}