using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;
    [Header("Flip Rotation Stats")]
    [SerializeField] private float _flipYRotationTime = 0.5f;
    private Coroutine _turnCoroutine;
    private Player _player;
    private bool _isFacingRight;

    private void Awake()
    {
        _player = _playerTransform.GetComponent<Player>();
        _isFacingRight = _player.isFacingRight;
    }

    private void Update()
    {
        transform.position = _playerTransform.position;
    }

    public void CallTurn()
    {
        _turnCoroutine = StartCoroutine(FlipYLerp());
    }
    
    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotation = DetermineEndRotation();
        float yRotation = 0f;
        
        float timeElapsed = 0f;
        while (timeElapsed < _flipYRotationTime)
        {
            timeElapsed += Time.deltaTime;
            
            yRotation = Mathf.Lerp(startRotation, endRotation, timeElapsed / _flipYRotationTime);
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
            
            yield return null;
        }
    }
    
    private float DetermineEndRotation()
    {
        _isFacingRight = !_isFacingRight;
        return _isFacingRight ? 0 : 180;
    }
}
