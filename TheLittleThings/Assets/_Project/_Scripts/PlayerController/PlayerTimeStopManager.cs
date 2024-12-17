using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeStopManager : MonoBehaviour
{
    public void HitStop(float time)
    {
        Time.timeScale = 0;
        StartCoroutine(ResumeTime(time));
    }
    IEnumerator ResumeTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }
}
