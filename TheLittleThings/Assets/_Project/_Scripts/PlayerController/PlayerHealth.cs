using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    [SerializeField] public PlayerHurt ouch;
    [SerializeField] public Player player;
    private int currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        player.stateMachine.SetState(ouch);
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
