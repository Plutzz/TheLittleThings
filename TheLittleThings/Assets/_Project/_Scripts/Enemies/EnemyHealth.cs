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

    public void TakeDamage() {
        
    }

    public void Die() {

    }

}
