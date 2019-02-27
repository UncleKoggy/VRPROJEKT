using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Grabing {
    public class LayerReader {

        SerializedObject OpenFile;
        SerializedProperty layers;
        public LayerReader() {
            OpenFile = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            layers = OpenFile.FindProperty("layers");
        }
        public SerializedProperty GetLayer(int layerIndex) {
            if (layers == null || !layers.isArray) {
                Debug.LogWarning("Can't set up the layers.  It's possible the format of the layers and tags data has changed in this version of Unity.");
                Debug.LogWarning("Layers is null: " + (layers == null));
                return null;
            }
            return layers.GetArrayElementAtIndex(layerIndex);
        }
        public void CreateLayer(int layerIndex, string layerName) {

            SerializedProperty layerSP = GetLayer(layerIndex);
            layerSP.stringValue = layerName;

            OpenFile.ApplyModifiedProperties();
        }

    }
}