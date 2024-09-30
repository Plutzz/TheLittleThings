using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float transitionSpeed = 10f;
    public float rotationSpeed = 10f;
    public float zoom = 2f;
    public CameraPoints cameraPoints;

    private List<CameraPlacement> cameraWaypoints;
    private int currentCameraPoint;

    void Start()
    {
        cameraWaypoints = cameraPoints.cameraLocations;
        currentCameraPoint = 0;
        SetCamera(currentCameraPoint);
    }

    void Update()
    {
        if (cameraWaypoints.Count > 0)
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    currentCameraPoint++;
                    if (currentCameraPoint > cameraWaypoints.Count - 1)
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
                        currentCameraPoint = cameraWaypoints.Count - 1;
                    }
                    SetCamera(currentCameraPoint);
                }
                Debug.Log(cameraWaypoints.Count);
                Debug.Log(currentCameraPoint);
            }
        } else
        {
            Debug.Log("No camera waypoints");
        }
    }

    private void SetCamera(int i)
    {
        transform.position = cameraWaypoints[i].position;
        transform.eulerAngles = cameraWaypoints[i].rotation;
    }

}
