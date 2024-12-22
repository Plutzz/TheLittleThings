using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public GameObject player;
    public int damage;
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == 8) {
            Debug.Log("ouch");
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }

    }
}