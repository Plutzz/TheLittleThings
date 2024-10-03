using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStopTrigger : MonoBehaviour
{
    [SerializeField] private float stopTime = 1f;
    [SerializeField] private float transitionTime = 0.5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("COLLISION" + collision);
        if(collision.TryGetComponent(out AutoScroll autoScroll))
        {
            StartCoroutine(autoScroll.StopTime(stopTime, transitionTime));
        }
    }
}
