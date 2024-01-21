using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PositionHandle2D)), CanEditMultipleObjects]
public class PositionHandle2DEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        PositionHandle2D example = (PositionHandle2D)target;

        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition = Handles.PositionHandle(example.targetPosition, Quaternion.identity);
        Handles.DrawLine(example.transform.position, newTargetPosition, 1f);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(example, "Change Look At Target Position");
            example.targetPosition = newTargetPosition;
            example.Update();
        }
    }
}