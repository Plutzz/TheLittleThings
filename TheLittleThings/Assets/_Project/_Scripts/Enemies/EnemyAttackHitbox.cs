using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRY TO DAMAGE " + other);
        if (other.TryGetComponent(out IDamageable damageable))
        {
            Debug.Log("DAMAGE " + damageable);
            damageable.TakeDamage(damage);
        }
    }
}
