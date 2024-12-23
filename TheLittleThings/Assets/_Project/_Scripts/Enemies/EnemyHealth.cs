using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using NaughtyAttributes;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject gameObjectToDestroy;
    [SerializeField] private int maxHealth;
    [field: SerializeField, ProgressBar("currentHealth", "maxHealth", EColor.Red)]
    private int currentHealth;

    [SerializeField] private GameObject healthBarFill;

    void Start() {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage) {
        
        UpdateHealthBar();
        
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

        if (healthBarFill != null)
        {
            healthBarFill.transform.localScale = new Vector3(0, healthBarFill.transform.localScale.y, healthBarFill.transform.localScale.z);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.transform.localScale = new Vector3(currentHealth/(float)maxHealth, healthBarFill.transform.localScale.y, healthBarFill.transform.localScale.z);
        }
    }
}
