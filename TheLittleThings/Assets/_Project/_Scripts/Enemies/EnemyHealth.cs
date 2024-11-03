using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject gameObjectToDestroy;
    [SerializeField] private int maxHealth;
    [field: SerializeField, ProgressBar("currentHealth", "maxHealth", EColor.Red)]
    private int currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            Die();
        }
    }

    public void TakeDamage(int damage,int invicibilityFrames) {

    }

    public void Die() {
        if (gameObjectToDestroy != null)
        {
            Destroy(gameObjectToDestroy);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
