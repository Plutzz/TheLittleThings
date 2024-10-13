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

    void Awake () {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.AddForce(rb.transform.forward * m_Speed);
        // rb.velocity = new Vector3(m_Speed * Mathf.Sign(rb.transform.forward.x), rb.velocity.y, rb.velocity.z);
        Destroy(gameObject, 1f);
    }
    void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject == Enemy) return;
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);  
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        } 
            Destroy(effect, 5f);
            Destroy(gameObject);
    }
}
