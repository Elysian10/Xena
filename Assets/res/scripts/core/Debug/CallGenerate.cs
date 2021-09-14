using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class CallGenerate : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Planet planet = (Planet)target;
        if (GUILayout.Button("Generate")){
            planet.TestStart();
        }
    }
}