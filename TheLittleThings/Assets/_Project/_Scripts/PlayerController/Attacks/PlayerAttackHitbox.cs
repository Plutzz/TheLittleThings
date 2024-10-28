using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    private List<Collider> previousHits;
    private Collider hitbox;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private Transform player;
    [HideInInspector] public int damage;
    [HideInInspector] public float knockback;

    private void Awake()
    {
        previousHits = new List<Collider>();
        hitbox = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        previousHits.Clear();
        hitbox.enabled = true;
    }
    private void OnDisable()
    {
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<IDamageable>() != null && !previousHits.Contains(collision))
        {
            previousHits.Add(collision);
            //collision.GetComponent<IDamageable>().TakeDamage(damage, knockback, transform.parent.position.x);
            collision.GetComponent<IDamageable>().TakeDamage(damage);
            Instantiate(hitEffectPrefab, collision.ClosestPointOnBounds(player.position + Vector3.up), Quaternion.identity);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
