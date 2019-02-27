using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Grabing
{
    public class GrabItemMultipleHands : MonoBehaviour, IGrabItem
    {

        public float GripStrength { get { return gripStrength; } set { gripStrength = value; } }
        public Rigidbody Rigid { get { return rigid; } set { rigid = value; } }
        [SerializeField]
        float gripStrength;
        [SerializeField]
        Rigidbody rigid;

        public int handsNeeded;

        List<Grabber> grabberRefs = new List<Grabber>();


        public void OnGrab(Grabber hand)
        {

            grabberRefs.Add(hand);

            if (grabberRefs.Count < handsNeeded) {
                return;
            }
            else if (grabberRefs.Count > handsNeeded)
                hand.joint = CreateJoint(hand.rigid);
            else
                foreach (Grabber A in grabberRefs) {
                    A.joint = CreateJoint(A.rigid);
                }

        }

        public void OnReachOut(Grabber A)
        {
            GrabRemove(A);
        }
        public void OnTriggerUp(Grabber A)
        {
            GrabRemove(A);
        }
        void GrabRemove(Grabber hand)
        {
            if (!grabberRefs.Contains(hand)) {
                return;
            }


            if (grabberRefs.Count == handsNeeded) {
                DestroyAll();
            }
            else {
                if (hand.joint != null)
                    Destroy(hand.joint);
                grabberRefs.Remove(hand);

            }


        }
        void DestroyAll()
        {

            rigid.gameObject.AddComponent<VelocityMaintain>().addrefs(rigid);
            foreach (Grabber A in grabberRefs) {
                Destroy(A.joint);
            }
            grabberRefs.Clear();

        }

        public Joint CreateJoint(Rigidbody baseRigid)
        {

            Joint joint = baseRigid.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = rigid;
            joint.breakForce = gripStrength;
            joint.enablePreprocessing = false;
            return joint;
        }

        public void Start()
        {
            if (rigid == null)
                rigid = transform.parent.GetComponent<Rigidbody>();
        }

        public void JointBroken(Grabber hand, float breakForce)
        {
        }

    }
}