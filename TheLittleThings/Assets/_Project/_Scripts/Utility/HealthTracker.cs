using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthTracker : MonoBehaviour
{
    public float MaxHP;
    public float CurrentHP
    {
        get; private set;
    }

    public bool IsVulnerable;

    private Dictionary<string, int> m_localIFrames;

    public delegate void OnDamaged(ref float amount, ref string damageSource, ref int localIFrameAddAmount);
    public event OnDamaged OnBeforeEntityDamaged;
    public event Action<float, string, int> OnEntityKilled;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxHP;
        m_localIFrames = new Dictionary<string, int>();
    }

    public void DamageEntity(float amount, string damageSource, int localIFrameAddAmount)
    {
        if (!IsVulnerable || m_localIFrames.ContainsKey(damageSource))
        {
            return;
        }

        OnBeforeEntityDamaged?.Invoke(ref amount, ref damageSource, ref localIFrameAddAmount);

        CurrentHP -= amount;

        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            OnEntityKilled?.Invoke(amount, damageSource, localIFrameAddAmount);
        }

        m_localIFrames.Add(damageSource, localIFrameAddAmount);
    }

    private void FixedUpdate()
    {
        for (int i = m_localIFrames.Count - 1; i >= 0; i--)
        {
            int iframeAmount = m_localIFrames[m_localIFrames.ElementAt(i).Key]--;
            if (iframeAmount < 0)
            {
                m_localIFrames.Remove(m_localIFrames.ElementAt(i).Key);
            }
        }
    }
}
