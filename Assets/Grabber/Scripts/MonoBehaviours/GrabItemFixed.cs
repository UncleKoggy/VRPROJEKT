using UnityEngine;
using System.Collections;

namespace Grabing
{
    public class GrabItemFixed : MonoBehaviour, IGrabItem
    {

        public float GripStrength { get { return gripStrength; } set { gripStrength = value; } }
        public Rigidbody Rigid { get { return rigid; } set { rigid = value; } }
        [SerializeField]
        float gripStrength;
        [SerializeField]
        Rigidbody rigid;

        bool grip = false;


        Joint joint;

        public void OnGrab(Grabber hand)
        {
            if (grip)
                Destroy(joint);
            else
                hand.joint = CreateJoint(hand.rigid);
            grip ^= true;
        }

        public void OnReachOut(Grabber A)
        {
            Destroy(joint);
        }

        public void OnTriggerUp(Grabber A)
        {

        }
        public Joint CreateJoint(Rigidbody baseRigid)
        {
            joint = baseRigid.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = rigid;
            joint.breakForce = gripStrength;
            joint.enablePreprocessing = false;
            return joint;
        }
        public void Start()
        {
            if (rigid == null)
                rigid = GetComponent<Rigidbody>();
        }
        public void JointBroken(Grabber hand, float breakForce)
        {
        }

    }
}