using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        ResetHP();
    }
    public void ResetHP()
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
            return;
        }

        m_localIFrames.Add(damageSource, localIFrameAddAmount);
    }

    private void FixedUpdate()
    {
        for (int i = m_localIFrames.Count - 1; i >= 0; i--)
        {
            int iframeAmount = m_localIFrames[m_localIFrames.ElementAt(i).Key]--;
            if (iframeAmount <= 0)
            {
                m_localIFrames.Remove(m_localIFrames.ElementAt(i).Key);
            }
        }
    }

    private void OnDrawGizmos()
    {
        #if UNITY_EDITOR
        if (Application.isPlaying)
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;
            
            StringBuilder b = new StringBuilder($"HP: {CurrentHP} IFrames: ");

            foreach (var iframeNum in m_localIFrames)
            {
                b.Append($"{iframeNum.Key}:{iframeNum.Value} ");
            }

            UnityEditor.Handles.Label(transform.position + Vector3.up * 2f, b.ToString(), style);

        }
        #endif
    }
}
