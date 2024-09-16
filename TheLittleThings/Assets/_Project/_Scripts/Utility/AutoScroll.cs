using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    public float ScrollAmount;
    // Start is called before the first frame update
    //void Awake()
    //{
    //    BoxCollider2D collider = deathHitbox.GetComponent<BoxCollider2D>();
    //}

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Vector3.right * ScrollAmount * Time.deltaTime;
    }
}
