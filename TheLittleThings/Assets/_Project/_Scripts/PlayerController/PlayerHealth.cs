using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    [field: SerializeField, ProgressBar("currentHealth", "maxHealth", EColor.Red)] private int currentHealth;
    public Player player;
    [SerializeField] private GameObject healthBarFill;
    void Start() {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        // Ignore getting hurt if the player is in roll state or hurt state
        if (player.stateMachine.currentState == player.roll || player.stateMachine.currentState == player.hurt) return;
    
        if (healthBarFill != null)
        {
            UpdateHealthBar();
        }
       
        
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

    private void UpdateHealthBar()
    {
        healthBarFill.transform.localScale = new Vector3(currentHealth/(float)maxHealth, healthBarFill.transform.localScale.y, healthBarFill.transform.localScale.z);
    }
}
