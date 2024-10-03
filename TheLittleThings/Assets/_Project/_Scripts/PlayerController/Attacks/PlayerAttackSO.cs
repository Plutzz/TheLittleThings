using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerAttacks/Normal Attack")]
public class PlayerAttackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOV;
    public int damage;
    public float knockback;
}
