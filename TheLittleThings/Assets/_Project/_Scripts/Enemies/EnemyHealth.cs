using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieHealth : MonoBehaviour, IDamageable
{
    int maxHealth;
    int currentHealth;

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
