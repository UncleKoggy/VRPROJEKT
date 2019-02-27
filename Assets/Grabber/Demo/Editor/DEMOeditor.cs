using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[UnityEditor.CustomEditor(typeof(DEMO))]
public class DEMOeditor : Editor {

    bool shown = false;
    private void OnEnable() {

        DEMO demo = (DEMO)target;
        if (!shown) {
            if (!demo.CheckLayer("Grabing") || !demo.CheckLayer("Walkable") || !demo.CheckLayer("Character")) {
                EditorUtility.DisplayDialog("ERROR", "Layers are not configured\n Use this Grabber Setup window to set up layers or do it manually", "ok");
                Grabing.GrabberWindow.ShowWindow();
            }
            shown = true;
        }
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        DEMO demo = (DEMO)target;
        GUILayout.Label("1.Make sure to set up layers using Grabber set up window");
        GUILayout.Label("2.Make sure that every GrabItem object and Controllers are in \"Grabing\" Layer");
        GUILayout.Label("3.Make sure that ground object is in \"Walkable\" Layer");
        GUILayout.Label("4.Make sure that CameraRig object is in \"Character\" Layer");
        GUILayout.Label("to achieve this you can use button below.(after setting up layers)");
        if (GUILayout.Button("Set objects into layers")) {
            demo.SetObjectToLayer(demo.transform.root, "GrabItem", "Grabing");
            demo.SetObjectToLayer(demo.transform.root, "Ground", "Walkable");
            demo.SetObjectToLayer(demo.transform.root, "[CameraRig]", "Character");

            demo.SetObjectToLayer(demo.transform.root, "Controller (left)", "Grabing");
            demo.SetObjectToLayer(demo.transform.root, "Controller (right)", "Grabing");
        }
    }
}
