using UnityEngine;
using System.Collections;

namespace Grabing
{
    public class VelocityMaintain : MonoBehaviour
    {

        Vector3 Velocity;
        Rigidbody rigid;
        bool a = true;
        public void addrefs(Rigidbody passed)
        {
            rigid = passed;
            Velocity = passed.velocity;
        }

        void FixedUpdate()
        {
            if (a) {
                rigid.velocity = Velocity * 1.5f;
                Destroy(this);
            }
            a = false;
        }
    }
}