using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRMind.Pointer {
    [RequireComponent(typeof(LineRenderer))]
    public class PointerLine : PointerSource {

        LineRenderer rend;
        void Awake() {
            rend = GetComponent<LineRenderer>();
            rend.useWorldSpace = true;
            OnRaycastBlocked += SetEndingPoint;
        }

        private void SetEndingPoint(GameObject a) {
            rend.SetPosition(1, hitPosition);
        }

        protected override void FixedUpdate() {
            rend.SetPosition(0, transform.position);
            rend.SetPosition(1, transform.position + transform.forward * length);
            base.FixedUpdate();
            if (target != null)
                SetEndingPoint(null);

        }
        public override void EnableAcquiring() {
            base.EnableAcquiring();
            rend.enabled = true;
        }

        public override void DisableAcquiring() {
            base.DisableAcquiring();
            rend.enabled = false;
        }



    }
}