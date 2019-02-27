using UnityEngine;
using System.Collections;

namespace Grabing {

    public class MockColliderGrabber : MonoBehaviour {

        public Grabber grabber;
        public GrabItemSnap mockGrab;
        void Start() {
            if (grabber == null)
                grabber = GetComponent<Grabber>();
        }
        void Update() {
            if (Input.GetKeyDown(KeyCode.W)) {

                grabber.Grab();
            }
            if (Input.GetKeyDown(KeyCode.S)) {
                grabber.Release();
            }
            if (Input.GetKeyDown(KeyCode.E)) {

                mockGrab.OnGrab(grabber);
            }
            if (Input.GetKeyDown(KeyCode.D)) {
                mockGrab.OnTriggerUp(grabber);
            }
        }

        void OnTriggerEnter(Collider collider) {
            grabber.AddTarget(collider.GetComponent<IGrabItem>());
        }

        void OnTriggerExit(Collider collider) {
            grabber.RemoveTarget(collider.GetComponent<IGrabItem>());
        }
    }
}