using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    private List<Collider> previousHits;
    private Collider hitbox;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerTimeStopManager timeStopManager;
    [SerializeField] private List<Transform> hitTransforms; // Holds a list of transforms that can be used for effects
    [SerializeField] public int hitTransformIndex; // Index of transform to use
    [HideInInspector] public float timeStopDuration;
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
        IDamageable damageable = collision.GetComponentInParent<IDamageable>();
        if (damageable != null && !previousHits.Contains(collision))
        {
            previousHits.Add(collision);
            damageable.TakeDamage(damage);
            //Instantiate(hitEffectPrefab, collision.ClosestPointOnBounds(player.position + Vector3.up), Quaternion.identity);
            Instantiate(hitEffectPrefab, hitTransforms[hitTransformIndex].position, Quaternion.identity);
            timeStopManager.HitStop(timeStopDuration);
        }
    }
}
