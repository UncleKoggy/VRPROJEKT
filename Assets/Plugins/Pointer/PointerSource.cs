using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRMind.Pointer {
    public class PointerSource : MonoBehaviour {


        public float length = 10;
        public LayerMask blockMask, interactionMask;
        public event Action<IPointItem> OnTargetAcquired, OnTargetLost;
        public IPointItem target { get; private set; }
        public Action<GameObject> OnRaycastBlocked;
        protected bool canAcquire = true;
        public Vector3 hitPosition { get; protected set; }
        public bool isBlocked { get; private set; }

        protected virtual void FixedUpdate() {
            if (canAcquire)
                AcquireTarget();
        }


        void AcquireTarget() {
            RaycastHit a;

            if (Physics.Raycast(new Ray(transform.position, transform.forward), out a, length, (interactionMask | blockMask))) {

                hitPosition = a.point;

                if (blockMask == (blockMask | 1 << a.collider.gameObject.layer)) {
                    isBlocked = true;
                    if (OnRaycastBlocked != null)
                        OnRaycastBlocked(a.collider.gameObject);
                    return;
                }

                IPointItem nTarget = a.collider.transform.GetComponent<IPointItem>();
                CheckTarget(nTarget);


            } else if (target != null) {
                TargetLost();
            }
            isBlocked = false;
        }

        void CheckTarget(IPointItem nTarget) {
            if (nTarget == target)
                return;

            if (target != null)
                TargetLost();

            TargetFound(nTarget);
        }

        void TargetFound(IPointItem nTarget) {
            target = nTarget;
            nTarget.OnPoint();
            if (OnTargetAcquired != null)
                OnTargetAcquired(nTarget);
        }

        void TargetLost() {
            target.OnEndPoint();
            if (OnTargetLost != null)
                OnTargetLost(target);
            target = null;
            hitPosition = Vector3.zero;
        }

        public virtual void DisableAcquiring() {
            if (target != null)
                TargetLost();
            canAcquire = false;
        }
        public virtual void EnableAcquiring() {
            canAcquire = true;
        }

    }
}