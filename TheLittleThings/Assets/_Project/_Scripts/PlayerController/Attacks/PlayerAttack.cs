using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : State
{
    /*
        Handles the attack animation
        Handles the attack force
    */
    [Header("Input")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Combo")]
    public List<PlayerAttackSO> combo;

    [Header("Attacks")]

    [SerializeField] private PlayerAttackManager attackManager;
    [SerializeField] private PlayerAttackHitbox attackHitbox;
    [SerializeField] private Animator anim;

    public event Action<int> playerCombo;
    public void PlayerComboTrigger(int comboNumber) { playerCombo?.Invoke(comboNumber); }


    /*
    NOTE: The combo attack will interrupt the roll state, so the player will stop mid
          way through the roll animation and start the attack animation. Don't know if
          this is what we want for the movement

          Attacking frame 1 and then jumping after will increment the combo counter.
          So the player can attack with the last part of the combo without having to go through the first 2 animations.
          Could be an issue if the last part of the combo does more damage than the first 2 parts.
    */


    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        rb.drag = 0;
        anim.Play("Stab", -1, 0);

    }

    public override void DoUpdateState()
    {
        int comboCounter = attackManager.ComboCount;

        if (comboCounter < 0 || comboCounter >= combo.Count)
        {
            return;
        }

        anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
        // attackHitbox.damage = combo[comboCounter].damage;
        // attackHitbox.knockback = combo[comboCounter].knockback;



        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            PlayerComboTrigger(comboCounter);
            
        }

        base.DoUpdateState();
    }

    public void SetComplete()
    {
        isComplete = true;
    }


}
