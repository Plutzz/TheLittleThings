using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    [Header("Flip Rotation Stats")]
    [SerializeField] private float flipYRotationTime = 0.5f;
    private bool isFacingRight;
    

    private void Awake()
    {
        isFacingRight = player.isFacingRight;
    }

    private void Update()
    {
        transform.position = player.transform.position;
    }

    public void CallTurn()
    {
        LeanTween.rotateY(gameObject, DetermineEndRotation(), flipYRotationTime).setEaseInOutSine();
    }
    
    private float DetermineEndRotation()
    {
        isFacingRight = !isFacingRight;
        return isFacingRight ? 0 : 180;
    }
}
