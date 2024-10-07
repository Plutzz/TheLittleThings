using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    float Hspeed = 28;
    public GameObject hitEffect;
    public GameObject Player;
    [SerializeField] private Rigidbody m_RigidBody;

    void Awake () {
        m_RigidBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        m_RigidBody.AddForce(m_RigidBody.transform.forward * Hspeed);
        Destroy(gameObject, 1f);
    }
    void OnCollisionEnter(Collision collision) {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
