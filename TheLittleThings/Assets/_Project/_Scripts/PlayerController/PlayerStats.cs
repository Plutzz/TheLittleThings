using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats™")]
public class PlayerStats : ScriptableObject
{
    public float MaxSpeed;
    public float NoInputDeceleration;
    public float JumpForce;
    public uint JumpFrameBufferAmount;

    public float Damage;
    public float AttackSpeedMult;
}