using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform pos1, pos2;
    [SerializeField] private int Speed;
    Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = pos2.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, pos1.position) < .1f) targetPos = pos2.position;
        if(Vector2.Distance(transform.position, pos2.position) < .1f) targetPos = pos1.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
