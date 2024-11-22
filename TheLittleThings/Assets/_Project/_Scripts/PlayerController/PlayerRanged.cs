using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;


public class PlayerRanged : MonoBehaviour
{
    public Camera cam;
    public GameObject bullet;
    public GameObject spawn;
    public Transform parent;

    // Update is called once per frame
    void Update()
    {
        rotate();
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            shoot();
        }
    }

    void rotate () {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = cam.ScreenToWorldPoint(mousePos);

        Vector2 dir = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y) * parent.localScale.x;
        transform.right = dir;
    }

    void shoot() {
        GameObject projectile = (GameObject)Instantiate(bullet, spawn.transform.position, Quaternion.identity);
        projectile.transform.right = transform.right * parent.localScale.x;

    }

}
