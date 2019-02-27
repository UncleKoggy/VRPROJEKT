using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Grabing;


public class DEMO : MonoBehaviour {

    private void Start() {
        CheckLayer("Grabing");
        CheckLayer("Walkable");
        CheckLayer("Character");
    }

    public bool CheckLayer(string a) {
        for (int i = 0; i < 32; i++) {
            if (LayerMask.LayerToName(i) == a)
                return true;
        }
        Debug.LogError("\"" + a + "\"" + " Layer can't be found");
        return false;
    }

    public void SetObjectToLayer(Transform a, string objectName, string layer) {
        if (a.gameObject.name == objectName)
            a.gameObject.layer = LayerMask.NameToLayer(layer);
        for (int i = 0; i < a.childCount; i++) {
            SetObjectToLayer(a.GetChild(i), objectName, layer);
        }
    }

}

