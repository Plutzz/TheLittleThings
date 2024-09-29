using UnityEngine;

[CreateAssetMenu(fileName = "CameraWayPoints", menuName = "Camera")]
public class CameraPoints : ScriptableObject
{
    public Vector3[] cameraLocation;
    public Vector3[] cameraRotation;
}
