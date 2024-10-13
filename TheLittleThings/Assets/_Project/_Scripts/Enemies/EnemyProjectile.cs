using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject Player;
    public PlayerHealth playerHealth;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float m_Speed;
    [SerializeField] private int damage;

    void Awake () {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        // rb.AddForce(rb.transform.forward * m_Speed);
        rb.velocity = new Vector3(m_Speed * Mathf.Sign(rb.transform.forward.x), rb.velocity.y, rb.velocity.z);
        Destroy(gameObject, 1f);
    }
    void OnCollisionEnter(Collision collision) {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        if(collision.gameObject.CompareTag("Player")) {
            playerHealth.TakeDamage(damage);
        }
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
