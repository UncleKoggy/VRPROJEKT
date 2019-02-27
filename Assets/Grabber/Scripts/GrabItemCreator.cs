using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Grabing {

    public enum GrabType { OneHanded, MultiHanded, Fixed, Snap, Climbing, Configurable, Diagnostics }

    public class GrabItemCreator {

        GameObject child;
       
        public GrabItemCreator(GrabType gType, float STR, int grabAmount, GameObject go, bool inChild) {
            Collider col = go.GetComponent<Collider>();
            if (col == null)
                col = go.AddComponent<BoxCollider>();

            if (inChild) {
                child = Children(go);
                copyCollider(col, child).isTrigger = true;
            } else
                child = go;
            child.layer = LayerMask.NameToLayer("Grabing");

            IGrabItem grabbing = AddGrabItem(gType, grabAmount);
            grabbing.GripStrength = STR;


            Rigidbody rigid = go.GetComponent<Rigidbody>();
            if (!rigid)
                rigid = go.AddComponent<Rigidbody>();

            grabbing.Rigid = rigid;
        }
        IGrabItem AddGrabItem(GrabType gType, int amount) {
            IGrabItem grabbing;
            switch (gType) {
                case GrabType.Fixed:
                    grabbing = child.AddComponent<GrabItemFixed>(); break;
                case GrabType.OneHanded:
                    grabbing = child.AddComponent<GrabItem>(); break;
                case GrabType.Snap:
                    grabbing = child.AddComponent<GrabItemSnap>(); break;
                case GrabType.MultiHanded:
                    grabbing = child.AddComponent<GrabItemMultipleHands>(); (grabbing as GrabItemMultipleHands).handsNeeded = amount; break;
                case GrabType.Climbing:
                    grabbing = child.AddComponent<GrabItemClimbing>(); break;
                case GrabType.Configurable:
                    grabbing = child.AddComponent<GrabItemConfigurable>(); break;

                default:
                    grabbing = null; break;
            }
            return grabbing;
        }

        Collider copyCollider(Collider col, GameObject target) {

            BoxCollider Z = col as BoxCollider;
            if (Z != null) {
                BoxCollider kolajder = target.AddComponent<BoxCollider>();
                kolajder.center = Z.center;
                kolajder.size = Z.size;
                return kolajder;
            }
            SphereCollider S = col as SphereCollider;
            if (S != null) {
                SphereCollider kolajder = target.AddComponent<SphereCollider>();
                kolajder.center = S.center;
                kolajder.radius = S.radius;
                return kolajder;
            }
            CapsuleCollider Q = col as CapsuleCollider;
            if (Q != null) {

                CapsuleCollider kolajder = target.AddComponent<CapsuleCollider>();
                kolajder.center = Q.center;
                kolajder.radius = Q.radius;
                kolajder.height = Q.height;
                kolajder.direction = Q.direction;
                return kolajder;
            }
            return null;
        }
        GameObject Children(GameObject father) {
            GameObject go = new GameObject("GrabItem");
            go.transform.parent = father.transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.rotation = father.transform.rotation;
            go.transform.localScale = new Vector3(1, 1, 1);
            return go;
        }
    }
}