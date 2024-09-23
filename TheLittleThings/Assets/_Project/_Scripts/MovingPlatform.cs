using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pos1, pos2;
    [SerializeField] private float Speed;
    private Vector2 targetPos;
    private Vector2 previousPosition;
    private Vector2 platformVelocity;
    [SerializeField] private float forceMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = pos2.position;
        previousPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, pos1.position) < .1f)
            targetPos = pos2.position;
        if(Vector2.Distance(transform.position, pos2.position) < .1f)
            targetPos = pos1.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);

        platformVelocity = ((Vector2)transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.transform.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.AddForce(new Vector2(platformVelocity.x * forceMultiplier, 0), ForceMode2D.Force);
            }
        }
    }
}