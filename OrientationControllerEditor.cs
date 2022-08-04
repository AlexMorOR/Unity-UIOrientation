using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(OrientationController))]
[CanEditMultipleObjects]
public class OrientationControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.Label("Current orientation: " +
            (OrientationController.isVertical ? "Vertical" : "Horizontal"));

        base.DrawDefaultInspector();

        var controllers = targets;

        if (GUILayout.Button("Save values"))
            foreach(var controller in controllers)
                ((OrientationController)controller).SaveCurrentState();

        if (GUILayout.Button("Put values"))
            foreach (var controller in controllers)
                ((OrientationController)controller).PutCurrentState();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
