using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

namespace Grabing {
    public class GrabberWindow : EditorWindow {


        int grabberLayer, characterLayer, walkableLayer;

        LayerReader layers;

        [MenuItem("Window/Grabber SetUp")]

        public static void ShowWindow() {

            EditorWindow.GetWindow(typeof(GrabberWindow), true, "Grabing");
        }


        void OnGUI() {
            layers = new LayerReader();

            EditorGUILayout.LabelField("Please select empty slots for layers");

            GUILayout.Space(20);
            GrabberDialog();

            GUILayout.Space(20);

            if (!LayerExists("Walkable")) {
                walkableLayer = EditorGUILayout.IntField("walkable layer index", walkableLayer);
                if (SetupLayer("Walkable", walkableLayer)) {
                    AllCollision(walkableLayer);
                    if (LayerExists("Grabing"))
                        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Grabing"), LayerMask.NameToLayer("Walkable"));
                }
            } else
                EditorGUILayout.LabelField("Walkable layer found at: " + LayerMask.NameToLayer("Walkable"));


            if (!LayerExists("Character")) {
                characterLayer = EditorGUILayout.IntField("character layer index", characterLayer);
                if (SetupLayer("Character", characterLayer))
                    ChangeMatrix(characterLayer, walkableLayer);
            } else
                EditorGUILayout.LabelField("Character layer found at: " + LayerMask.NameToLayer("Character"));


            if (!ClimbingExists()) {
                if (GUILayout.Button("Set up climbing")) {
                    AddClimber();
                }
            } else
                EditorGUILayout.LabelField("Climber already exists at scene!");

        }

        void GrabberDialog() {
            if (!LayerExists("Grabing")) {
                grabberLayer = EditorGUILayout.IntField("Grabing layer index", grabberLayer);
                if (SetupLayer("Grabing", grabberLayer))
                    ChangeMatrix(grabberLayer, grabberLayer);
            } else {
                EditorGUILayout.LabelField("Grabing layer found at: " + LayerMask.NameToLayer("Grabing"));
                if (!GrabberExists()) {
                    if (GUILayout.Button("Set up grabber"))
                        AddGrabber();
                } else
                    EditorGUILayout.LabelField("Grabber already exists at scene!");

            }
        }

        bool GrabberExists() {
            foreach (SteamVR_TrackedObject a in FindObjectsOfType<SteamVR_TrackedObject>()) {
                if (a.GetComponent<Grabber>() != null)
                    return true;
            }
            return false;
        }
        bool ClimbingExists() {
            GameObject a = FindObjectOfType<SteamVR_PlayArea>().gameObject;
            if (a.GetComponent<Climbing.Climber>() != null)
                return true;
            return false;
        }
        bool LayerExists(string name) {
            for (int i = 0; i < 32; i++) {
                if (LayerMask.LayerToName(i) == name)
                    return true;
            }
            return false;
        }
        bool SetupLayer(string layerName, int layerIndex) {
            if (ValueInRange(layerIndex))
                if (!LayerExists(layerName))
                    if (!InUse(layerIndex))
                        if (GUILayout.Button("Set up layer")) {
                            layers.CreateLayer(layerIndex, layerName);
                            return true;
                        }
            return false;
        }
        bool InUse(int index) {
            if (layers.GetLayer(index).stringValue == "") {
                return false;
            }
            return true;
        }

        bool ValueInRange(int layerIndex) {
            if (layerIndex > 31) {
                EditorGUILayout.LabelField("layer number can not exceed 31");
                return false;
            } else if (layerIndex < 0) {
                EditorGUILayout.LabelField("layer number can not be lower than 0");
                return false;
            }
            return true;
        }

        void SetUpGrabber(GameObject a) {
            a.AddComponent<SphereCollider>().radius = .1f;
            Rigidbody rigid;
            if (a.GetComponent<Rigidbody>() == null)
                rigid = a.AddComponent<Rigidbody>();
            else rigid = a.GetComponent<Rigidbody>();
            rigid.isKinematic = true;
            rigid.useGravity = false;
            rigid.angularDrag = 0;
            a.AddComponent<colliderGrabber>();
            a.AddComponent<Grabber>();
            a.layer = grabberLayer;
        }

        void AddGrabber() {
            if (LayerMask.NameToLayer("Grabing") == -1) {
                EditorUtility.DisplayDialog("Grabber", "Layer not found", "OK");
                return;
            }
            GameObject room = FindObjectOfType<SteamVR_PlayArea>().transform.root.gameObject;
            if (room == null) {
                Debug.LogError("Could not found CameraRig");
                return;
            }


            Transform[] children = room.GetComponentsInChildren<Transform>(true);
            foreach (Transform a in children) {
                if (a.name.Contains("left"))
                    SetUpGrabber(a.gameObject);
                else if (a.name.Contains("right"))
                    SetUpGrabber(a.gameObject);
            }
            EditorUtility.DisplayDialog("Grabber", "Set up complete", "roger that!");
        }
        void AddClimber() {
            GameObject room = FindObjectOfType<SteamVR_PlayArea>().transform.root.gameObject;

            if (room == null) {
                Debug.LogError("Could not found CameraRig");
                return;
            }
            if (LayerMask.NameToLayer("Walkable") == -1) {
                EditorUtility.DisplayDialog("Climber", "Layer not found", "OK");
                return;
            }
            room.layer = LayerMask.NameToLayer("Character");
            Rigidbody rigid = AddRigid(room);
            room.AddComponent<BoxCollider>().size = new Vector3(.3f, .1f, .3f);
            room.AddComponent<Climbing.CharacterFootCollider>();
            room.AddComponent<Climbing.Climber>();
            EditorUtility.DisplayDialog("Climber", "Set up complete", "roger that!");
        }

        void ChangeMatrix(int layerIndex, int layerAccepted) {
            for (int i = 0; i < 32; i++) {
                Physics.IgnoreLayerCollision(i, layerIndex);
            }

            Physics.IgnoreLayerCollision(layerAccepted, layerIndex, false);
        }
        void AllCollision(int layerIndex) {
            for (int i = 0; i < 32; i++) {
                Physics.IgnoreLayerCollision(i, layerIndex, false);
            }
        }

        Rigidbody AddRigid(GameObject obj) {
            Rigidbody rigid = obj.GetComponent<Rigidbody>();
            if (!rigid)
                rigid = obj.AddComponent<Rigidbody>();
            rigid.angularDrag = 0f;
            rigid.freezeRotation = true;
            return rigid;

        }

        void LogWarning(string A) {
            EditorUtility.DisplayDialog("Error", A, "OK");
        }
    }
}