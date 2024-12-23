using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SleepState : State
{
    [FormerlySerializedAs("wakeUpAnimClip")] [SerializeField] private AnimationClip sleepAnimClip;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        rb.velocity = Vector3.zero;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isComplete = true;
        }
    }
}
