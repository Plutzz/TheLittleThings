using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float transitionSpeed = 10f;
    public float rotationSpeed = 10f;
    public float zoom = 2f;
    public int startingCameraPointIndex = 0;
    public CameraPoints cameraPoints;



    private Vector3[] cameraLocations;
    private Vector3[] cameraRotations;
    private int currentCameraPoint;

    void Start()
    {
        cameraLocations = cameraPoints.cameraLocation;
        cameraRotations = cameraPoints.cameraRotation;
        currentCameraPoint = startingCameraPointIndex;
        transform.position = cameraLocations[currentCameraPoint];
        transform.eulerAngles = cameraRotations[currentCameraPoint];
    }

    void Update()
    {
        if (cameraLocations.Length == cameraRotations.Length)
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    currentCameraPoint++;
                    if (currentCameraPoint > cameraLocations.Length - 1)
                    {
                        currentCameraPoint = 0;
                    }
                    SetCamera(currentCameraPoint);
                }
                else
                {
                    currentCameraPoint--;
                    if (currentCameraPoint < 0)
                    {
                        currentCameraPoint = cameraLocations.Length - 1;
                    }
                    SetCamera(currentCameraPoint);
                }
            }
        } else
        {
            Debug.Log("Amount of camera locations and rotations are not the same size");
        }
    }

    private void SetCamera(int i)
    {
        transform.position = cameraLocations[i];
        transform.eulerAngles = cameraRotations[i];
    }

}
