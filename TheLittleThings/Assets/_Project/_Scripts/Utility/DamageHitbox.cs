using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageHitbox : MonoBehaviour
{
    //public List<Collider2D> ActiveColliders;
    public float DamageAmount;
    public string DamageSource;
    public int LocalIFrameAddAmount;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out HealthTracker hp))
            hp.DamageEntity(DamageAmount, DamageSource, LocalIFrameAddAmount);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HealthTracker hp))
            hp.DamageEntity(DamageAmount, DamageSource, LocalIFrameAddAmount);
    }
}