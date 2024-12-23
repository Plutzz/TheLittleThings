using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SocialPlatforms.Impl;
public class PlayerComboAttack : State
{
    [SerializeField] private float attackTime = 1f;
    public float attackMoveAmount = 0.5f;
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
        //Debug.Log("Learn combo" + attackManager.FinalAttack);
        base.DoEnterLogic();
        isComplete = false;
        timer = attackTime;
        rb.drag = groundDrag;
        rb.velocity = Vector2.zero;
        
        // Set animation to length of attack
        animator.StartPlayback();
        animator.speed = 0;
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
        // If our hitbox needs to be enabled, enable the hitbox and give player the correct force in their forward direction
        if (stateUptime > timeBeforeHitboxActive && !attackManager.AttackPoint.activeInHierarchy)
        {
            attackManager.AttackPoint.SetActive(true);
            rb.AddForce(playerObj.forward * attackMoveAmount, ForceMode.Impulse);
        }
        // Allow the player to rotate before the attack hit box is active
        // this gives the player more control over where their attack is landing
        else if(stateUptime <= timeBeforeHitboxActive)
        {
            Vector2 _inputVector = playerInput.moveVector;
            Vector3 _inputDir = orientation.forward * _inputVector.y + orientation.right * _inputVector.x;
            playerObj.transform.forward = Vector3.Slerp(playerObj.forward, _inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        
        float _time = Utilites.Map(stateUptime, 0, attackManager.attackLength, 0, 1, true);
        animator.Play("Attack", 0, _time);
    }

    public void SetIsComplete(bool _isComplete)
    {
        isComplete = _isComplete;
    }
}