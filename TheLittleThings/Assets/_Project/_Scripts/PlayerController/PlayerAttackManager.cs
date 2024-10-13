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
    [SerializeField] private PlayerAttack playerAttack;

    [Header("Attacks")]
    [SerializeField] private float comboInputTimeThreshold = 1f;
    [SerializeField] private float lastInputTime;
    [SerializeField] private int comboCount = 0;

    Dictionary<int, bool> hasExecuted = new Dictionary<int, bool>();

    // need to use this to iterate over and modify the dictionary
    List<int> comboNumbers = new List<int>();
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
        PerformCombo();
    }

    public void PerformCombo()
    {
        if (comboCount >= playerAttack.combo.Count)
        {
            comboCount = 0;
            ResetExecutionTracker();
        }
        print("Performing Combo " + comboCount);
        if (comboCount == 0 || (comboCount < playerAttack.combo.Count -1 && hasExecuted[comboCount]))
        {
            comboCount++;
        }
        else if (comboCount >= playerAttack.combo.Count)
        {
            Debug.Log("Restart Combo!");
            ResetExecutionTracker();
        }

        lastInputTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastInputTime > comboInputTimeThreshold)
        {
            // if too much time has passed reset combo
            ResetExecutionTracker();
        }
    }
}
