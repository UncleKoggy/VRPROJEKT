using UnityEngine;
using System.Collections;

namespace Grabing
{
    public class GrabItem : MonoBehaviour, IGrabItem
    {

        public float GripStrength { get { return gripStrength; } set { gripStrength = value; } }
        public Rigidbody Rigid { get { return rigid; } set { rigid = value; } }

        [SerializeField]
        float gripStrength;
        [SerializeField]
        Rigidbody rigid;


        public void OnGrab(Grabber hand)
        {
            if (hand.joint != null)
                return;
            hand.joint = CreateJoint(hand.rigid);
        }

        public void OnReachOut(Grabber A)
        {
            Destroy(A.joint);
        }

        public void OnTriggerUp(Grabber A)
        {
            Destroy(A.joint);
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