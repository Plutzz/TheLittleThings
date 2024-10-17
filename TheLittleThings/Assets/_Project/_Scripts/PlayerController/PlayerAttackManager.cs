using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    /*
        Handles what the combo attack they are currently on
        Handles the attack cooldown
        Handles the attack buffer window
    */
    [Header("Player Components")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerAttack playerAttack;

    [Header("Attacks")]
    [SerializeField] private float comboInputTimeThreshold = 1f;
    [SerializeField] private float lastInputTime;
    [SerializeField] private int comboCount = 0;

    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float lastComboEnd;
    [SerializeField] private float timeBetweenCombos = 0.5f;
    [SerializeField] private float lastBufferedAttack; // the time the last buffered attack was performed
    [SerializeField] private Transform sprite;
    [SerializeField] private float attackForce = 10f;
    [SerializeField] private Player player;

    Dictionary<int, bool> hasExecuted = new Dictionary<int, bool>();

    // need to use this to iterate over and modify the dictionary
    List<int> comboNumbers = new List<int>();
    bool isBuffered = false;
    public int ComboCount { get { return comboCount; } }

    void Start()
    {
        for (int i = 1; i <= playerAttack.combo.Count; i++)
        {
            hasExecuted.Add(i, false);
        }
        comboNumbers = new List<int>(hasExecuted.Keys);
        playerAttack.playerCombo += UpdateTheHasExecutedDictionary;
    }

    public void ResetExecutionTracker()
    {
        comboCount = 0;
        foreach (var comboNumber in comboNumbers) { hasExecuted[comboNumber] = false; }
    }

    private void OnDestroy()
    {
        playerAttack.playerCombo -= UpdateTheHasExecutedDictionary;
    }

    void UpdateTheHasExecutedDictionary(int comboNumber)
    {
        hasExecuted[comboNumber] = true;
    }

    public void PerformCombo()
    {
        if (Time.time - lastInputTime >= attackCooldown)
        {
            print("Performing Combo " + comboCount);
            if (comboCount == 0 || (comboCount < playerAttack.combo.Count && hasExecuted[comboCount]))
            {
                comboCount++;
                if (sprite.localScale.x > 0)
                    player.rb.AddForce(player.transform.right * attackForce, ForceMode.Impulse);
                else
                    player.rb.AddForce(-player.transform.right * attackForce, ForceMode.Impulse);
            }

            if (comboCount >= playerAttack.combo.Count)
            {
                Debug.Log("Restart Combo!");
                ResetExecutionTracker();
            }
            lastInputTime = Time.time;
        }
        else
        {
            isBuffered = true;
            lastBufferedAttack = Time.time;
        }



    }

    void Update()
    {
        if (Time.time - lastInputTime > comboInputTimeThreshold)
        {
            // if too much time has passed reset combo
            playerAttack.SetComplete();
            ResetExecutionTracker();
        }


        if (isBuffered && Time.time - lastBufferedAttack > comboInputTimeThreshold)
        {
            print("Buffered Attack Expired");
            isBuffered = false;
        }

        if (isBuffered && Time.time - lastComboEnd > timeBetweenCombos && Time.time - lastInputTime >= attackCooldown)
        {
            print("Buffered Attack");
            PerformCombo();
            isBuffered = false;
        }
    }
}
