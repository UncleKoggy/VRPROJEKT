using UnityEngine;
using System.Collections;

namespace Grabing {
    public class GrabItemSnap : MonoBehaviour, IGrabItem {

        public float GripStrength { get { return gripStrength; } set { gripStrength = value; } }
        public Rigidbody Rigid { get { return rigid; } set { rigid = value; } }
        [SerializeField]
        float gripStrength;
        [SerializeField]
        Rigidbody rigid;
        public Vector3 localOffset, localRot;



        public void OnGrab(Grabber hand) {
            if (hand.joint != null)
                return;
            SnapObject(hand.transform);
            hand.joint = CreateJoint(hand.rigid);
        }

        void SnapObject(Transform hand) {
            rigid.transform.position = hand.position + hand.TransformDirection(localOffset);
            rigid.transform.rotation = hand.rotation * Quaternion.Euler(localRot);
        }
        public void OnReachOut(Grabber A) {
            Destroy(A.joint);
        }

        public void OnTriggerUp(Grabber A) {
            Destroy(A.joint);
        }

        public Joint CreateJoint(Rigidbody baseRigid) {

            Joint joint = baseRigid.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = rigid;
            joint.breakForce = gripStrength;
            joint.enablePreprocessing = false;
            joint.breakTorque = gripStrength;
            return joint;
        }
        public void Start() {
            if (rigid == null)
                rigid = transform.parent.GetComponent<Rigidbody>();
            gameObject.layer = LayerMask.NameToLayer("Grabing");
        }
        public void JointBroken(Grabber hand, float breakForce) {
        }

    }
}