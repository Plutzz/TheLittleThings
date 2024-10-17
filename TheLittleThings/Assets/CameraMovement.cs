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
                    CameraPlacement p1 = cameraWaypoints[currentCameraPoint];
                    CameraPlacement p2 = cameraWaypoints[(currentCameraPoint + 1) % cameraWaypoints.Count];
                    currentCameraPoint = (currentCameraPoint + 2) % cameraWaypoints.Count;
                    CameraPlacement p3 = cameraWaypoints[currentCameraPoint];
                    
                    StartCoroutine(MoveCamera(p1, p2, p3));
                }
                else
                {
                    CameraPlacement p1 = cameraWaypoints[currentCameraPoint];
                    CameraPlacement p2 = cameraWaypoints[(currentCameraPoint - 1) % cameraWaypoints.Count];
                    currentCameraPoint = (currentCameraPoint - 2) % cameraWaypoints.Count;
                    CameraPlacement p3 = cameraWaypoints[currentCameraPoint];
                    
                    StartCoroutine(MoveCamera(p1, p2, p3));
                }
                Debug.Log(cameraWaypoints.Count);
                Debug.Log(currentCameraPoint);
            }
            if (Input.GetMouseButtonDown(0))
            {
                currentCameraPoint += 1;
                currentCameraPoint = currentCameraPoint % cameraWaypoints.Count;
                SetCamera(currentCameraPoint);
            }
        } else
        {
            Debug.Log("No camera waypoints");
        }
    }

    struct PosAndRot
    {
        public Vector3 position;
        public Vector3 rotation;
        public PosAndRot(CameraPlacement placement)
        {
            position = placement.position;
            rotation = placement.rotation;
        }
        public PosAndRot(Transform t)
        {
            position = t.position;
            rotation = t.eulerAngles;
        }
        public PosAndRot(Vector3 pos, Vector3 rot)
        {
            position = pos;
            rotation = rot;
        }

        public static PosAndRot BezierTransform(PosAndRot t1, PosAndRot t2, PosAndRot t3, float t)
        {
            return new PosAndRot(BezierTransformVector(t1.position, t2.position, t3.position, t),
                BezierTransformRotation(t1.rotation, t2.rotation, t3.rotation, t));
        }

        private static Vector3 BezierTransformVector(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            float tminus = 1 - t;
            // (1-t)^2*p0+2*t*(1-t)*p1+t^2*p2, simplified from Lerp(Lerp(p0,p1,t),Lerp(p1,p2,t),t)
            
            return tminus * tminus * p0 + 2 * t * tminus * p1 + t * t * p2;
        }
        private static Vector3 BezierTransformRotation(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            float tminus = 1 - t;
            // (1-t)^2*p0+2*t*(1-t)*p1+t^2*p2, simplified from Lerp(Lerp(p0,p1,t),Lerp(p1,p2,t),t)
            return LerpRotation(LerpRotation(p0, p1, t), LerpRotation(p1, p2, t), t);
        }

        private static Vector3 LerpRotation(Vector3 p0, Vector3 p1, float t)
        {

            return new Vector3(LerpAngle(p0.x, p1.x, t), LerpAngle(p0.y, p1.y, t), LerpAngle(p0.z, p1.z, t));
        }

        private static float LerpAngle(float a0, float a1, float t)
        {
            a0 = a0 % 360;
            a1 = a1 % 360;


            if (a1 - a0 > 180)
            {
                a0 += 360;
            }
            else if (a0 - a1 > 180)
            {
                a1 += 360;
            }

            return Mathf.Lerp(a0, a1, t);
        }
    }
    IEnumerator MoveCamera(CameraPlacement c1, CameraPlacement c2, CameraPlacement c3)
    {
        PosAndRot t1 = new PosAndRot(c1);
        PosAndRot t2 = new PosAndRot(c2);
        PosAndRot t3 = new PosAndRot(c3);
        float t = 0;
        while (t < 1)
        {
            PosAndRot bezierPoint = PosAndRot.BezierTransform(t1, t2, t3, t);
            SetCamera(bezierPoint);
            t += Time.deltaTime;
            yield return null;
        }
        SetCamera(t3);

        yield break;
    }
    private void SetCamera(int i)
    {
        transform.position = cameraWaypoints[i].position;
        transform.eulerAngles = cameraWaypoints[i].rotation;
    }

    private void SetCamera(PosAndRot t)
    {
        transform.position = t.position;
        transform.eulerAngles = t.rotation;
    }

}
