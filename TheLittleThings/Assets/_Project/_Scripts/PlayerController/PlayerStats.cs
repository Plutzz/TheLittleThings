using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats�")]
public class PlayerStats : ScriptableObject
{
    public float MaxSpeed;
    public float Acceleration;
    public float CurrentGravity;
    public float NormalGravity;
    public float WallGravity;
    public float NoInputDeceleration;

    public float JumpForce;
    public uint JumpFrameBufferAmount;
    public float FallSpeedLimit;

    public float RollSpeed;
    public float RollDuration;
    public float RollDrag;


    public float Damage;
    public float AttackSpeedMult;
    
}