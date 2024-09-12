using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitbox : MonoBehaviour
{
    public List<Collider2D> ActiveColliders;

    public float DamageAmount;
    public string DamageSource;
    public int LocalIFrameAddAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ActiveColliders.Contains(collision.collider) && isActiveAndEnabled)
        {
            HealthTracker hp = collision.gameObject.GetComponent<HealthTracker>();

            hp.DamageEntity(DamageAmount, DamageSource, LocalIFrameAddAmount);
        }
    }
}