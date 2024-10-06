using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        if (currentHealth < 0) {
            Die();
        }
    }

    public void TakeDamage(int damage,int invicibilityFrames) {

    }

    public void Die() {
        Destroy(gameObject);
    }
}
