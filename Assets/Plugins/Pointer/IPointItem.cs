using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VRMind.Pointer {
    public interface IPointItem {
        GameObject gameObject { get; }
        Transform transform { get; }
        void OnPoint();
        void OnEndPoint();
        void OnPick();
    }
}
