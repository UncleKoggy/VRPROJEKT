using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//zaimplementować OnJointBreak()
namespace Grabing {

    [RequireComponent(typeof(colliderGrabber), typeof(Rigidbody), typeof(Collider))]
    public class Grabber : MonoBehaviour {

        List<IGrabItem> objs = new List<IGrabItem>();

        [HideInInspector]
        public Rigidbody rigid;

        [HideInInspector]
        public Joint joint;

        void Start() {
            rigid = GetComponent<Rigidbody>();
        }
        public void AddTarget(IGrabItem A) {
            objs.Add(A);

        }
        public void RemoveTarget(IGrabItem S) {
            if (S == objs[0])
                objs[0].OnReachOut(this);

            objs.Remove(S);
        }

        public void Release() {
            if (objs.Count != 0)
                objs[0].OnTriggerUp(this);
        }
        public void Grab() {
            if (objs.Count != 0)
                objs[0].OnGrab(this);
        }
        public void Grab(IGrabItem target) {
            target.OnGrab(this);
        }
        void OnJointBreak(float breakForce) {
            if (objs.Count != 0)
                objs[0].JointBroken(this, breakForce);
        }
    }
}