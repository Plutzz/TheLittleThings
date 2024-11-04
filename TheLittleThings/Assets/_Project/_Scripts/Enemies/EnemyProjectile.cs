using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject Enemy;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float m_Speed;
    [SerializeField] private int damage;
    [SerializeField] private float duration;

    void Awake () {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.AddForce(rb.transform.forward * m_Speed);
        Destroy(gameObject, duration);
    }
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        } 
    }
}
