using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldAttack : State
{
    [SerializeField] private float maxHoldTimer = 2f;
    [SerializeField] private float chargeTime = 0f;

    [SerializeField] private State holdChargeup;
    [SerializeField] private HoldDoAttack holdAttack;

    private int minDamage = 1;
    private int maxDamage = 10;
    private int currentDamage;

    private bool isHolding = false;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Entering HoldAttack State");

        currentDamage = minDamage;
        chargeTime = 0f;

        SetState(holdChargeup);
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();

        chargeTime += Time.deltaTime;

        currentDamage = Mathf.RoundToInt(Mathf.Lerp(minDamage, maxDamage, chargeTime / maxHoldTimer));

        Debug.Log("Charging Damage");

        if (Input.GetMouseButtonUp(0) || chargeTime >= maxHoldTimer)
        {
            holdAttack.damage = currentDamage;
            SetState(holdAttack);
        }
    }
}

