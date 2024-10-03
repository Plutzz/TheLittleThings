using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraPoints))]
public class CameraPointsSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Reference to the ScriptableObject
        CameraPoints cameraPoints = (CameraPoints)target;

        // Show default fields (position, rotation)
        DrawDefaultInspector();

        // Create a field for dragging and dropping a Transform
        Transform selectedTransform = (Transform)EditorGUILayout.ObjectField("Drop Transform Here", null, typeof(Transform), true);

        // If a transform was dropped, update the ScriptableObject's data
        if (selectedTransform != null)
        {
            // Update the data within the ScriptableObject
            cameraPoints.AddLocation(selectedTransform);

            // Mark the ScriptableObject as dirty so the changes are saved
            EditorUtility.SetDirty(cameraPoints);
        }
    }
}
