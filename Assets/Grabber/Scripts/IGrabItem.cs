using UnityEngine;
using System.Collections;

namespace Grabing
{
    public interface IGrabItem
    {
        float GripStrength { get; set; }
        Rigidbody Rigid { get; set; }

        void OnGrab(Grabber hand);
        void OnReachOut(Grabber hand);
        void OnTriggerUp(Grabber hand);
        void JointBroken(Grabber hand, float brakForce);
        Joint CreateJoint(Rigidbody baseRigid);
        
    }
}