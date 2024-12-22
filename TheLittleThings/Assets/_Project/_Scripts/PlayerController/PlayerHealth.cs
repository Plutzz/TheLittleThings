using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    public Player player;
    private int currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        player.stateMachine.SetState(player.hurt); 
        currentHealth -= damage;
        if (currentHealth <= 0) {
            Die();
        }
    }

    public void TakeDamage(int damage,int invicibilityFrames) {

    }

    public void Die() {
        Destroy(gameObject);
    }
}
