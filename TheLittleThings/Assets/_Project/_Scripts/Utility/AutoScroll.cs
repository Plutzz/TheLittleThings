using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    public float ScrollAmount;
    public GameObject CameraTarget;
    public float CameraScrollYValue;
    // Start is called before the first frame update
    //void Awake()
    //{
    //    BoxCollider2D collider = deathHitbox.GetComponent<BoxCollider2D>();
    //}

    // Update is called once per frame
    void Update()
    {
        float newXVal = gameObject.transform.position.x + ScrollAmount * Time.deltaTime;

        float newYVal = gameObject.transform.position.y;

        float ydiff = CameraTarget.transform.position.y - gameObject.transform.position.y;

        if (ydiff > CameraScrollYValue)
        {
            newYVal += (Math.Abs(ydiff) - CameraScrollYValue) / 200f;
        }
        else if (-ydiff > CameraScrollYValue)
        {
            newYVal -= (Math.Abs(ydiff) - CameraScrollYValue) / 200f;
        }

        gameObject.transform.position = new Vector3(newXVal, newYVal, gameObject.transform.position.z);
    }

    public IEnumerator StopTime(float _time, float _slowdownTime)
    {
        float _initScrollAmount = ScrollAmount;
        float _initTime = Time.time;
        while(ScrollAmount > 0)
        {
            Debug.Log(ScrollAmount);
            ScrollAmount = Mathf.Lerp(_initScrollAmount, 0, (Time.time - _initTime) / _slowdownTime);
            yield return null;
        }
        yield return new WaitForSeconds(_time);
        _initTime = Time.time;
        while (ScrollAmount < _initScrollAmount)
        {
            Debug.Log(ScrollAmount);
            ScrollAmount = Mathf.Lerp(0, _initScrollAmount, (Time.time - _initTime) / _slowdownTime);
            yield return null;
        }
        ScrollAmount = _initScrollAmount;
    }

}
