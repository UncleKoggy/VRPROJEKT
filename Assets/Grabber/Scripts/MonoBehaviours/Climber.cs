using UnityEngine;
using System.Collections;

namespace Climbing {
    public class Climber : MonoBehaviour {

        public enum Controltype { full, vertical, horizontalHandyCap };
        public Controltype controls = Controltype.full;


        int Grip;
        public float translationMultiplayer = 2;
        Transform lHand, rHand, room;
        Vector3 LLpos, RLpos;
        Rigidbody body;


        void Start() {

            room = FindObjectOfType<SteamVR_PlayArea>().transform;
            if (room == null)
                Debug.LogError("SteamVr room not found");
            body = room.GetComponent<Rigidbody>();
            Transform[] children = room.GetComponentsInChildren<Transform>(true);
            foreach (Transform a in children) {
                if (a.name.Contains("left"))
                    lHand = a;
                else if (a.name.Contains("right"))
                    rHand = a;
            }

            if (body == null)
                Debug.LogError("SteamVr room not found");
        }
        void LateUpdate() {

            if (Grip == 2) {
                Climb();
            }
            LLpos = lHand.position;
            RLpos = rHand.position;
        }
        void Climb() {
            Vector3 offset = lHand.position - LLpos;
            offset += rHand.position - RLpos;
            switch (controls) {
                case Controltype.full:
                    room.position -= offset / 2; return;

                case Controltype.vertical:
                    room.position -= Vector3.Project(offset / 2, Vector3.up); return;

                case Controltype.horizontalHandyCap:
                    room.position -= new Vector3(offset.x / (translationMultiplayer * 2), offset.y / 2, offset.z / (translationMultiplayer * 2)); return;
            }


        }
        public void AddGrip() {
            Grip++;
            body.useGravity = false;
            body.velocity = Vector3.zero;
        }
        public void RemoveGrip() {
            Grip--;
            if (Grip == 0)
                body.useGravity = true;
        }

    }
}