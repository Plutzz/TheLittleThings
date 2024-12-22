using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }
    }
}
