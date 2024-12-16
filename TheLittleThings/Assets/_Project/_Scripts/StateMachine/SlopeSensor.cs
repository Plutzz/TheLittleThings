using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeSensor : MonoBehaviour
{
    [SerializeField] private float rayLength;
    public LayerMask groundLayer;
    public bool isOnSlope { get; private set; }
    public RaycastHit hit;
    // TODO: Add maximum slope angle check
    private void Update()
    {
        CheckSlope();
    }

    private void CheckSlope()
    {
        isOnSlope = Physics.Raycast(transform.position, Vector3.down, out hit, rayLength, groundLayer);
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);
    }
}
