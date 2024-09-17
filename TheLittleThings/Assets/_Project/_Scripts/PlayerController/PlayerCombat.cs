using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : State
{
    public AnimationClip attackAnimation;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Attack();
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        if(stateUptime > attackAnimation.length)
        {
            isComplete = true;
        }
    }

    void Attack()
    {
        animator.Play("Attack1");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
        }
    }
}
