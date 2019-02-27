using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Grabing {

    public class GrabItemWindow : EditorWindow {
        [SerializeField]
        float GripStrength = Mathf.Infinity;
        [SerializeField]
        GrabType Type;
        [SerializeField]
        int amount;

        private static Color pro = new Color(0.5f, 0.7f, 0.3f, 1f);
        private static Color free = new Color(0.2f, 0.3f, 0.1f, 1f);

        private GUIStyle style = new GUIStyle();



        [MenuItem("Window/Grab Item")]
        public static void ShowWindow() {
            EditorWindow.GetWindow(typeof(GrabItemWindow));
        }

        void OnGUI() {

            style.wordWrap = true;
            style.normal.textColor = EditorGUIUtility.isProSkin ? pro : free;

            EditorGUILayout.Space();

            Type = (GrabType)EditorGUILayout.EnumPopup(Type);
            if (Type == GrabType.MultiHanded) {
                amount = EditorGUILayout.IntField("Hand Amount", amount);
            }
            GripStrength = EditorGUILayout.FloatField("Break Force", GripStrength);
            EditorGUILayout.LabelField("copies collider from this object");

            if (GUILayout.Button("Set Up in children")) {
                foreach (GameObject s in Selection.gameObjects) {
                    new GrabItemCreator(Type, GripStrength, amount, s, true);
                }
            }
            if (GUILayout.Button("Set Up as Grabbable")) {
                foreach (GameObject s in Selection.gameObjects) {
                    new GrabItemCreator(Type, GripStrength, amount, s, false);
                }
            }
        }

    }
}
