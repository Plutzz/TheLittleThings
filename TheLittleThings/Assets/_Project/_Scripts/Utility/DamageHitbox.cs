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
        Debug.Log(collision + " Trigger");
        if(collision.gameObject.TryGetComponent(out HealthTracker hp))
            hp.DamageEntity(DamageAmount, DamageSource, LocalIFrameAddAmount);
    }
}