using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{

    [Header("Combo")]
    [SerializeField] private List<PlayerAttackSO> combo;
    [SerializeField] private float continueComboTimer = 0.2f;
    [SerializeField] private float timeBetweenCombos = 1f;

    private float lastComboEnd;
    private int comboCounter;

    [Header("Attacks")]
    [SerializeField] private float attackBufferWindow = 0.2f;
    [SerializeField] private float normalizedTime = 1f;
    private float attackCooldown = 0.5f;
    private bool bufferedAttack;
    private float lastBufferedAttack;
    private float lastAttackTime;
    public GameObject AttackPoint;
    public bool FinalAttack;


    [SerializeField] private Player player; 
    [SerializeField] private PlayerAttackHitbox attackHitbox;
    [SerializeField] private PlayerInput inputManager;
    [SerializeField] private Animator anim;



    private void Start()
    {
        attackHitbox = GetComponentInChildren<PlayerAttackHitbox>(true);
        //player = GetComponent<Player>();
        //inputManager = GetComponent<InputManager>();
        
    }

    public void Update()
    {
        if (inputManager.attackPressedDownThisFrame && player.stateMachine.currentState is not PlayerAirborne3D)
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

        ExitAttack();
    }


    void Attack()
    {
        if (Time.time - lastComboEnd > timeBetweenCombos && comboCounter <= combo.Count)
        {
            CancelInvoke("IncompleteCombo");

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                //attackHitbox.sfxName = combo[comboCounter].sfxName;
                attackHitbox.damage = combo[comboCounter].damage;
                attackHitbox.knockback = combo[comboCounter].knockback;
                player.attack.comboAttack.timeBeforeHitboxActive = combo[comboCounter].timeBeforeHitboxActive;
                attackCooldown = combo[comboCounter].cooldownAfterAttack;
                anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
                player.stateMachine.SetState(player.attack, true);


                anim.Play("Attack", 0, 0);
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

    }

    private void ExitAttack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) return; 
        
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > normalizedTime)
        {
            Invoke("IncompleteCombo", continueComboTimer);

            player.attack.comboAttack.SetIsComplete(true);
        }
    }

    private void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = lastAttackTime;
        FinalAttack = false;
    }

    private void IncompleteCombo()
    {
        FinalAttack = false;
        comboCounter = 0;
    }
}