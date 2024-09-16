using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HealthTracker Health;
    public float MaxSpeed;
    public float NoInputDeceleration;
    public float JumpForce;
    public uint JumpFrameBufferAmount;

    public float Damage;
    public float AttackSpeedMult;
}