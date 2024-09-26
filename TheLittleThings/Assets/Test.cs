using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Rigidbody2D body;
    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(5, body.velocity.y);
    }
}
