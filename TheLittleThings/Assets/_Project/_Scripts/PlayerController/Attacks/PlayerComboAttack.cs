using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SocialPlatforms.Impl;
public class PlayerComboAttack : State
{
    [SerializeField] private float attackTime = 1f;
    [SerializeField] private float attackMoveAmount = 0.5f;
    [SerializeField] private float finalAttackMoveAmount = 50f;
    [SerializeField] private float groundDrag = 2f;
    [SerializeField] private float rotationSpeed = 1f;
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
        Debug.Log("Learn combo" + attackManager.FinalAttack);
        base.DoEnterLogic();
        isComplete = false;
        timer = attackTime;
        rb.drag = groundDrag;
        rb.velocity = Vector2.zero;
        
        // Set animation to length of attack
        
        animator.StartPlayback();
        animator.playbackTime = 0;
        animator.speed = 0;
        
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
        animator.StopPlayback();
        animator.speed = 1;
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        if (stateUptime > timeBeforeHitboxActive)
        {
            attackManager.AttackPoint.SetActive(true);
        }
        else 
        {
            Vector2 _inputVector = new Vector2(playerInput.xInput, playerInput.yInput);
            Vector3 _inputDir = orientation.forward * _inputVector.y + orientation.right * _inputVector.x;
            playerObj.transform.forward = Vector3.Slerp(playerObj.forward, _inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        
        float _time = Map(stateUptime, 0, attackManager.attackLength, 0, 1, true);
        animator.Play("Attack", 0, _time);
    }

    public void SetIsComplete(bool _isComplete)
    {
        isComplete = _isComplete;
    }
    
    
    private float Map(float _value, float _min1, float _max1, float _min2, float _max2, bool _clamp = false)
    {
        float _val = _min2 + (_max2 - _min2) * ((_value - _min1) / (_max1 - _min1));
        return _clamp ? Mathf.Clamp(_val, Mathf.Min(_min2, _max2), Mathf.Max(_min2, _max2)) : _val;
    }
    
}