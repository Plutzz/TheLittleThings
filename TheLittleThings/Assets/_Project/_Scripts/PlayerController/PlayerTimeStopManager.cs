using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeStopManager : MonoBehaviour
{
    [SerializeField] private float pauseTimeScale = 0.1f;
    public void HitStop(float time)
    {
        Time.timeScale = pauseTimeScale;
        StartCoroutine(ResumeTime(time));
    }
    IEnumerator ResumeTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }
}
