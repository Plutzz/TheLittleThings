using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;


public class EnemyRanged : MonoBehaviour
{
    public GameObject spike;
    public Transform spawn;
    public float time = 0f;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 2f) {
            shoot();
            time = 0f;
        }
    }
    void shoot() {
        GameObject projectile = (GameObject)Instantiate(spike, spawn.transform.position, spawn.rotation);
    }
}
