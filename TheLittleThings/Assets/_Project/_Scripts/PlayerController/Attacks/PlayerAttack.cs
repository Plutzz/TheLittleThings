using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : State
{
    [Header("Input")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Combo")]
    [SerializeField] private List<PlayerAttackSO> combo;
    [SerializeField] private float continueComboTimer = 0.2f;
    [SerializeField] private float timeBetweenCombos = 1f;

    private float lastComboEnd;
    public int comboCounter;

    [Header("Attacks")]
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackBufferWindow = 0.2f;
    private bool bufferedAttack;
    private float lastBufferedAttack;
    private float lastAttackTime;
    public GameObject AttackPoint;
    public bool FinalAttack;
    [SerializeField] private Player player;
    [SerializeField] private PlayerStats stats => player.stats;

    [SerializeField] private PlayerAttackHitbox attackHitbox;
    [SerializeField] private Animator anim;


/*
NOTE: The combo attack will interrupt the roll state, so the player will stop mid
      way through the roll animation and start the attack animation. Don't know if
      this is what we want for the movement

      Attacking frame 1 and then jumping after will increment the combo counter.
      So the player can attack with the last part of the combo without having to go through the first 2 animations.
      Could be an issue if the last part of the combo does more damage than the first 2 parts.
*/

    public override void DoUpdateState()
    {
        if (playerInput.attackPressedThisFrame)
        {
            Attack();
        }

        // Handle buffered attacks
        if (bufferedAttack && Time.time - lastBufferedAttack > attackBufferWindow)
        {
            bufferedAttack = false;
        }

        if (bufferedAttack && Time.time - lastComboEnd > timeBetweenCombos && Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
            bufferedAttack = false;
        }


        base.DoUpdateState();
        ExitAttack();
    }

    void Attack()
    {

        if (Time.time - lastComboEnd > timeBetweenCombos && comboCounter <= combo.Count)
        {
            CancelInvoke(nameof(IncompleteCombo));

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                anim.runtimeAnimatorController = combo[comboCounter].animatorOV;

                // attackHitbox.damage = combo[comboCounter].damage;
                // attackHitbox.knockback = combo[comboCounter].knockback;

                anim.Play("Attack" + (comboCounter + 1));
                rb.velocity = Vector2.zero;

                comboCounter++;

                if (comboCounter == combo.Count - 1)
                {
                    FinalAttack = true;
                }
                if (comboCounter >= combo.Count)
                {
                    EndCombo();
                }
                lastAttackTime = Time.time;
            }
            else
            {
                bufferedAttack = true;
                lastBufferedAttack = Time.time;
            }
        }
        else
        {
            bufferedAttack = true;
            lastBufferedAttack = Time.time;
        }

        ExitAttack();
    }


    void ExitAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            isComplete = true;
            Invoke(nameof(IncompleteCombo), continueComboTimer);
        }
    }

    private void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = lastAttackTime;
        FinalAttack = false;
        DoExitLogic();
    }

    private void IncompleteCombo()
    {
        FinalAttack = false;
        comboCounter = 0;
        DoExitLogic();
    }

    public override void DoExitLogic()
    {
        isComplete = true;  
        base.DoExitLogic();
    }
}
