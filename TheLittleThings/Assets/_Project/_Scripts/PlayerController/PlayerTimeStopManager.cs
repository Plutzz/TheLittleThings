using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeStopManager : MonoBehaviour
{
    [SerializeField] private float pauseTimeScale = 0.1f;
    
    /// <summary>
    /// Freezes gameTime for "time" seconds. To be used as a hit stop effect
    /// </summary>
    /// <param name="time"></param>
    public void HitStop(float time)
    {
        Time.timeScale = pauseTimeScale;
        StartCoroutine(ResumeTime(time));
    }
    /// <summary>
    /// Time will resume after time seconds
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator ResumeTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }
}
