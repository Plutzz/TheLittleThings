using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CameraWayPoints", menuName = "CameraPlacement")]
public class CameraPoints : ScriptableObject
{
    public List<CameraPlacement> cameraLocations = new List<CameraPlacement>();

    public void AddLocation(Transform transform)
    {
        CameraPlacement currCam = new CameraPlacement();
        currCam.GetOrientation(transform);
        cameraLocations.Add(currCam);
    }
}

[System.Serializable]
public class CameraPlacement
{
    public string name; //does nothing, just for easier readability in editor
    public Vector3 position;
    public Vector3 rotation;

    public void GetOrientation(Transform transform)
    {
        this.position = transform.position;
        this.rotation = transform.eulerAngles;
    }
}
