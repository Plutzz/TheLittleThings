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
    public float attackLength {get; private set;}
    public GameObject AttackPoint;
    public bool FinalAttack;


    [SerializeField] private Player player; 
    [SerializeField] private PlayerAttackHitbox attackHitbox;
    [SerializeField] private PlayerInput inputManager;
    private Animator anim;



    private void Awake()
    {
        attackHitbox = GetComponentInChildren<PlayerAttackHitbox>(true);
        anim = player.animator;
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


    /// <summary>
    /// Attempts to perform an attack
    /// </summary>
    void Attack()
    {
        if (player.stateMachine.currentState == player.hurt) return;
        
        // Attack is inputted before combo cooldown or combo is not complete
        if (Time.time - lastComboEnd <= timeBetweenCombos || comboCounter > combo.Count)
        {
            // Buffer Attack
            bufferedAttack = true;
            lastBufferedAttack = Time.time;
            return;
        }
        
        CancelInvoke("IncompleteCombo");
        
        // Attack is pressed before attackCooldown is up
        if (Time.time - lastAttackTime < attackCooldown)
        {
            // Buffer Attack
            bufferedAttack = true;
            lastBufferedAttack = Time.time;
            return;
        }
        
        
        // Perform attack
        
        AssignValues();
                
        player.stateMachine.SetState(player.attack, true);
                
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


    /// <summary>
    /// Assigns values of current combo's scriptable object to places that need values.
    /// </summary>
    private void AssignValues()
    {
        PlayerAttackSO attackScriptableObject = combo[comboCounter];
        
        attackHitbox.damage = attackScriptableObject.damage;
        attackHitbox.knockback = attackScriptableObject.knockback;
        attackHitbox.timeStopDuration = attackScriptableObject.timeStopDuration;
        attackHitbox.hitTransformIndex = attackScriptableObject.hitTransformIndex;
        player.attack.comboAttack.timeBeforeHitboxActive = attackScriptableObject.timeBeforeHitboxActive;
        player.attack.comboAttack.attackMoveAmount = attackScriptableObject.moveAmount;
        attackCooldown = attackScriptableObject.cooldownAfterAttack;
        attackLength = attackScriptableObject.attackLength;
        anim.runtimeAnimatorController = attackScriptableObject.animatorOV;
    }

    /// <summary>
    /// Sets the player's attack state to complete when attack is done
    /// </summary>
    private void ExitAttack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) return; 
        
        
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > normalizedTime)
        {
            Invoke("IncompleteCombo", continueComboTimer);
            player.attack.comboAttack.SetIsComplete(true);
        }
    }

    /// <summary>
    /// Ends the combo when the player finished all attacks
    /// </summary>
    private void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = lastAttackTime;
        FinalAttack = false;
    }

    /// <summary>
    /// Resets the combo when the player doesn't attack soon enough to continue the combo
    /// </summary>
    private void IncompleteCombo()
    {
        FinalAttack = false;
        comboCounter = 0;
    }
}