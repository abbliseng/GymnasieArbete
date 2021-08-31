using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(DrawOrbits))]
public class DebugDrawOrbitsEditor : Editor
{
    override public void OnInspectorGUI () {
        DrawOrbits drawOrbits = (DrawOrbits)target;
        if (GUILayout.Button("Draw Orbits")) {
            drawOrbits.Draw();
        }
        DrawDefaultInspector();
    }
}
