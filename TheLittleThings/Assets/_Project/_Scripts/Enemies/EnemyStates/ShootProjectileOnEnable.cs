using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectileOnEnable : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform muzzleLocation;
    private void OnEnable()
    {
        GameObject _proj = Instantiate(projectilePrefab, muzzleLocation.position, Quaternion.identity);
        _proj.transform.forward = transform.forward;
    }
}
