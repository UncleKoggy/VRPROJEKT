using UnityEngine;
using System.Collections;

namespace Grabing
{
    public class colliderGrabber : MonoBehaviour
    {

        Grabber grabber;
        SteamVR_TrackedObject device;

        void Start()
        {
            device = GetComponent<SteamVR_TrackedObject>();
            if (grabber == null)
                grabber = GetComponent<Grabber>();
        }
        void Update()
        {

            if (SteamVR_Controller.Input((int)device.index).GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
                grabber.Grab();

            }
            if (SteamVR_Controller.Input((int)device.index).GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
                grabber.Release();
            }

        }

        void OnTriggerEnter(Collider collider)
        {
            grabber.AddTarget(collider.GetComponent<IGrabItem>());
        }

        void OnTriggerExit(Collider collider)
        {
            grabber.RemoveTarget(collider.GetComponent<IGrabItem>());
        }
    }
}