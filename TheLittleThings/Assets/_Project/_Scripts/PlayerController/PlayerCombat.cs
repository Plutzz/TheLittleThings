using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : State
{
    public AnimationClip attackAnimation;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public GameObject hitParticles;
    public Transform spriteObject;

    public float damage = 2.0f;

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
            if(enemy.TryGetComponent(out HealthTracker enemyHealth))
            {
                Debug.Log("Damage " + enemy.name);
                enemyHealth.DamageEntity(damage, "Player", 1);
                Instantiate(hitParticles, enemy.transform.position, spriteObject.localScale.x < 0 ? Quaternion.Euler(180, 90, 90) : Quaternion.Euler(0, 90, 90));
            }
        }
    }
}
