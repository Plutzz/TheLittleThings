using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 3D wall sensor
/// </summary>
public class WallSensor : MonoBehaviour
{
    public bool OnWall { get; private set; }
    [SerializeField] private float rayLength;
    public LayerMask wallLayer;

    private void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        OnWall = Physics.Raycast(transform.position, transform.forward, rayLength, wallLayer);
        Debug.DrawRay(transform.position, transform.forward * rayLength);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * rayLength);
    }
}
