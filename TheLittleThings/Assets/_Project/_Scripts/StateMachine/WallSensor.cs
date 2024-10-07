using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSensor : MonoBehaviour
{
    public bool wallLeft { get; private set; }
    public bool wallRight { get; private set; }
    [SerializeField] private float rayLength;
    public LayerMask groundLayer;

    private void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        wallLeft = Physics.Raycast(transform.position, Vector3.left, rayLength, groundLayer);
        wallRight = Physics.Raycast(transform.position, Vector3.right, rayLength, groundLayer);
        Debug.DrawRay(transform.position, transform.right * rayLength);
        Debug.DrawRay(transform.position, -transform.right * rayLength);
    }
}
