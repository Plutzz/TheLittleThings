using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttacks : State
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
    private float currentAnimAttackTime;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        // Movement
        rb.drag = 4;
        rb.velocity = Vector2.zero;


    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();

        
        if (Time.time - lastAttackTime >= currentAnimAttackTime)
        {
            isComplete = true;
        }

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
            print("Buffered Attack");
            Attack();
            bufferedAttack = false;
        }

        ExitAttack();
    }

    public override void CheckTransitions()
    {
        base.CheckTransitions();

        // if (playerInput.xInput != 0)
        // {
        //     stateMachine.SetState(player.move);
        // }
        // else if (playerInput.ctrlPressedThisFrame)
        // {
        //     stateMachine.SetState(player.roll);
        // }
    }

    void Attack()
    {

        if (Time.time - lastComboEnd > timeBetweenCombos && comboCounter <= combo.Count)
        {
            CancelInvoke(nameof(DoExitLogic));

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
                currentAnimAttackTime = anim.runtimeAnimatorController.animationClips[comboCounter].length;

                // attackHitbox.damage = combo[comboCounter].damage;
                // attackHitbox.knockback = combo[comboCounter].knockback;

                anim.Play("Attack" + (comboCounter + 1));


                comboCounter++;

                if (comboCounter == combo.Count - 1)
                {
                    FinalAttack = true;
                }
                if (comboCounter >= combo.Count)
                {
                    // Combo is complete
                    lastComboEnd = lastAttackTime;
                    DoExitLogic();
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
            Invoke(nameof(DoExitLogic), continueComboTimer);
        }
    }


    public override void DoExitLogic()
    {
        isComplete = true;
        base.DoExitLogic();
    }

    public override void ResetValues()
    {
        base.ResetValues();
        comboCounter = 0;
        FinalAttack = false;
    }
}
