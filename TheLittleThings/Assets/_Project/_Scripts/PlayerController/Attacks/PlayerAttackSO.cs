using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "PlayerAttacks/Normal Attack")]
public class PlayerAttackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOV;
    public int damage;
    public float knockback;
    public float timeBeforeHitboxActive;
    public float attackLength = 1f;
    public float cooldownAfterAttack = 1f;
    public float moveAmount = 0f;
    public float timeStopDuration;
}
