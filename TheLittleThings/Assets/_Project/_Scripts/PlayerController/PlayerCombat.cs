using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : State
{
    public AnimationClip attackAnimation;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float chargeTime = 0.5f;

    private bool isCharging = false;
    private float chargeTimer = 0f;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        isCharging = false;
        chargeTimer = 0f;
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();

        if (Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            chargeTimer = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            chargeTimer += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isCharging)
            {
                if (chargeTimer >= chargeTime)
                {
                    ChargeAttack();
                }
                else
                {
                    RegularAttack();
                }
                isCharging = false;
            }
        }

        if (stateUptime > attackAnimation.length)
        {
            isComplete = true;
        }
    }

    void RegularAttack()
    {
        animator.Play("Attack1");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Regular Attack hit: " + enemy.name);
        }
    }

    void ChargeAttack()
    {
        animator.Play("ChargeAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange * 1.5f, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Charge Attack hit: " + enemy.name);
        }
    }

    void Attack()
    {
        if (isCharging && chargeTimer >= chargeTime)
        {
            ChargeAttack();
        }
        else
        {
            RegularAttack();
        }
    }
}
