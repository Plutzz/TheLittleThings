using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{

    [Header("Combo")]
    [SerializeField] private List<PlayerAttackSO> combo;
    [SerializeField] private float continueComboTimer = 0.2f;
    [SerializeField] private float timeBetweenCombos = 1f;
    
    private float lastComboEnd;
    private int comboCounter;

    [Header("Attacks")]
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackBufferWindow = 0.2f;
    private bool bufferedAttack;
    private float lastBufferedAttack;
    private float lastAttackTime;
    public GameObject AttackPoint;
    public bool FinalAttack;


    private StateMachine stateMachine;
    private Player player;
    private PlayerAttackHitbox attackHitbox;
    [SerializeField] private Animator anim;
    


    private void Start()
    {
        attackHitbox = GetComponentInChildren<PlayerAttackHitbox>(true);
        player = GetComponent<Player>();
        stateMachine = player.stateMachine;
    }

    public void Update()
    {
        //if (inputManager.AttackPressedThisFrame)
        //{
        //    Attack();
        //}

        // Handle buffered attacks
        if(bufferedAttack && Time.time - lastBufferedAttack > attackBufferWindow)
        {
            bufferedAttack = false;
        }

        if(bufferedAttack && Time.time - lastComboEnd > timeBetweenCombos && Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
            bufferedAttack = false;
        }

        ExitAttack();
    }


    void Attack()
    {
        if(Time.time - lastComboEnd > timeBetweenCombos && comboCounter <= combo.Count)
        {
            CancelInvoke("IncompleteCombo");

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                //stateMachine.ChangeState(player.states["Attack"]);
                anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
                attackHitbox.damage = combo[comboCounter].damage;
                attackHitbox.knockback = combo[comboCounter].knockback;
                
                anim.Play("Attack", 0, 0);
                comboCounter++;
                if(comboCounter == combo.Count - 1)
                {
                    FinalAttack = true;
                }
                if(comboCounter >= combo.Count)
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

    }

    void ExitAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("IncompleteCombo", continueComboTimer);

            //if (player.inputManager.MoveInput == 0)
            //{
            //    stateMachine.ChangeState(player.states["Idle"]);
            //}
            //else
            //{
            //    stateMachine.ChangeState(player.states["Move"]);
            //}
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = lastAttackTime;
        FinalAttack = false;
    }

    void IncompleteCombo()
    {
        FinalAttack = false;
        comboCounter = 0;
    }
}
