using UnityEngine;

namespace Grabing {
    public class GrabItemConfigurable : MonoBehaviour, IGrabItem {

        public float GripStrength { get { return gripStrength; } set { gripStrength = value; } }
        public Rigidbody Rigid { get { return rigid; } set { rigid = value; } }

        [SerializeField]
        float gripStrength;

        [SerializeField]
        Rigidbody rigid;
        public float positionSpring = 20000f, positionDamper = 20f, maximumForce = 200;

        public void OnGrab(Grabber hand) {
            if (hand.joint != null)
                return;
            hand.joint = CreateJoint(hand.rigid);
        }

        public void OnReachOut(Grabber A) {
            Destroy(A.joint);
        }

        public void OnTriggerUp(Grabber A) {
            Destroy(A.joint);
        }

        public Joint CreateJoint(Rigidbody baseRigid) {

            Joint joint = baseRigid.gameObject.AddComponent<ConfigurableJoint>();
            joint.connectedBody = Rigid;
            joint.breakForce = gripStrength;
            joint.enablePreprocessing = false;
            JointConfiguration(joint as ConfigurableJoint);
            return joint;
        }
        public void Start() {
            if (rigid == null)
                rigid = GetComponent<Rigidbody>();
        }
        public void JointBroken(Grabber hand, float breakForce) {

        }

        void JointConfiguration(ConfigurableJoint joint) {


            joint.xDrive = configureJointDrive(gripStrength * positionSpring, gripStrength * positionDamper, gripStrength * maximumForce);
            joint.yDrive = configureJointDrive(gripStrength * positionSpring, gripStrength * positionDamper, gripStrength * maximumForce);
            joint.zDrive = configureJointDrive(gripStrength * positionSpring, gripStrength * positionDamper, gripStrength * maximumForce);

        }


        JointDrive configureJointDrive(float positionSpring, float positionDamper, float maximumForce) {
            JointDrive jointDrive = new JointDrive();
            jointDrive.positionSpring = positionSpring;
            jointDrive.positionDamper = positionDamper;
            jointDrive.maximumForce = maximumForce;
            return jointDrive;
        }



    }
}