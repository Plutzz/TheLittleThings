using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    public float ScrollAmount;
    public bool IsPaused;
    public GameObject CameraTarget;
    // Start is called before the first frame update
    //void Awake()
    //{
    //    BoxCollider2D collider = deathHitbox.GetComponent<BoxCollider2D>();
    //}

    // Update is called once per frame
    void Update()
    {
        float newXVal = IsPaused ? gameObject.transform.position.x : gameObject.transform.position.x + ScrollAmount * Time.deltaTime;

        gameObject.transform.position = new Vector3(newXVal, CameraTarget.transform.position.y, gameObject.transform.position.z);
    }
}
