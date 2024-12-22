using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera lockOnCamera;
    [SerializeField] private CinemachineFreeLook thirdPersonCamera;
    [SerializeField] private Transform playerLookAt;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float thirdPersonResetOffset;
    private void Update()
    {
        if (playerInput.toggleCameraReleasedThisFrame)
        {
            ToggleCameraMode();
        }
    }


    private void ToggleCameraMode()
    {
        if (thirdPersonCamera.gameObject.activeInHierarchy)
        {
            EnableLockOnCamera();
        }
        else
        {
            EnableThirdPersonCamera();
        }
    }

    private void EnableThirdPersonCamera()
    {
        thirdPersonCamera.m_XAxis.Value = playerLookAt.eulerAngles.y + thirdPersonResetOffset;
        thirdPersonCamera.gameObject.SetActive(true);
        lockOnCamera.gameObject.SetActive(false);
    }
    private void EnableLockOnCamera()
    {
        lockOnCamera.gameObject.SetActive(true);
        thirdPersonCamera.gameObject.SetActive(false);
    }
    
    
}
