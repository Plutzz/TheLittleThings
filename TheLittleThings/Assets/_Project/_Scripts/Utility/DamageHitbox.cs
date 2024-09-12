using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitbox : MonoBehaviour
{
    public List<Collider2D> ActiveColliders;

    public bool IsActive;

    public float DamageAmount;
    public string DamageSource;
    public int LocalIFrameAddAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ActiveColliders.Contains(collision.collider) && IsActive)
        {
            HealthTracker hp = collision.gameObject.GetComponent<HealthTracker>();

            hp.DamageEntity(DamageAmount, DamageSource, LocalIFrameAddAmount);
        }
    }
}