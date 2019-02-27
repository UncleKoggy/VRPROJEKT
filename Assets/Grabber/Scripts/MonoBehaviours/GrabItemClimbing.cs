using UnityEngine;
using System.Collections;
namespace Grabing
{
    public class GrabItemClimbing : MonoBehaviour, IGrabItem
    {
        public float GripStrength { get { return gripStrength; } set { gripStrength = value; } }
        public Rigidbody Rigid { get { return rigid; } set { rigid = value; } }

        [SerializeField]
        float gripStrength;
        [SerializeField]
        Rigidbody rigid;
        Climbing.Climber climber;

        public void OnGrab(Grabber hand)
        {
            if (hand.joint != null)
                return;
            hand.joint = CreateJoint(hand.rigid);
            climber.AddGrip();

        }

        public void OnReachOut(Grabber A)
        {
            if (A.joint != null)
                climber.RemoveGrip();
            Destroy(A.joint);
        }

        public void OnTriggerUp(Grabber A)
        {
            if (A.joint != null)
                climber.RemoveGrip();
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
            climber = FindObjectOfType<Climbing.Climber>();
        }

        public void JointBroken(Grabber hand, float breakForce)
        {

        }




    }
}