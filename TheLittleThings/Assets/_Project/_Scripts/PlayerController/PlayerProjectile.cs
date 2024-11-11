using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    float Hspeed = 28;
    public GameObject hitEffect;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Hspeed * Time.deltaTime);
        Destroy(gameObject, 1f);

    }

    void OnCollisionEnter(Collision collision) {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
