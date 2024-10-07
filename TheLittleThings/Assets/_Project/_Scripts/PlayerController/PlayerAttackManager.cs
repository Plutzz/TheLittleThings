using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    /*
        Handles what the combo attack they are currently on
        Handles the attack cooldown
        Handles the attack buffer window
    */
    [Header("Player Components")]
    [SerializeField] private Player player;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerAttack playerAttack;



    [Header("Attacks")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackBufferWindow;
    private float lastAttackTime;
    private bool bufferedAttack;
    private float lastBufferedAttack;
    private float lastComboEnd;
    private float timeBetweenCombos = 1.0f;
    private int comboLength;
    public int currentComboAttack { get; private set; }
    private bool isAttacking => IsAttackAnimationPlaying();

    void Start()
    {
        currentComboAttack = 0;
        lastAttackTime = Time.time;
        lastComboEnd = Time.time;
        comboLength = playerAttack.combo.Count;
    }

    void Update()
    {
        if (playerInput.attackPressedDownThisFrame && !isAttacking)
        {
            lastAttackTime = Time.time;
            bufferedAttack = true;
            lastBufferedAttack = Time.time;

            // Debug.Log($"Buffered attack at {Time.time}");
            // Debug.Log($"Last attack time: {lastAttackTime}");
        }

        // Handle buffered attacks
        if (bufferedAttack && Time.time - lastBufferedAttack > attackBufferWindow)
        {
            bufferedAttack = false;
        }

        if (bufferedAttack && player.stateMachine.currentState is PlayerAttack && Time.time - lastComboEnd > timeBetweenCombos && Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
            bufferedAttack = false;
        }

        // Reset combo attack if attack cooldown has passed
        if (Time.time - lastAttackTime >= attackCooldown && !IsAttackAnimationPlaying())
        {
            currentComboAttack = 0;
        }
    }


    void Attack()
    {
        Debug.Log($"Attacking with combo attack {currentComboAttack}");

        lastAttackTime = Time.time;

        currentComboAttack++; 

        Debug.Log($"Combo attack incremented to {currentComboAttack}");

        if (currentComboAttack >= comboLength)
        {
            currentComboAttack = 0;
            lastComboEnd = Time.time;
            Debug.Log("Combo attack reset");
        }
    }


    bool IsAttackAnimationPlaying()
    {
        // Check if the current animation state is tagged as "Attack" and is still playing
        return anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;
    }
}
