using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFinished : MonoBehaviour {

    public static LoadFinished Instance { get; private set; }
    public event Action Loaded;
    public bool isStopped = false;
    public Rigidbody core;
    private void Awake()
    {
        Instance = this;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "stop")
        {
            isStopped = true;
            if (Loaded != null)
                Loaded();
        }
    }
    public void Update()
    {
        if (isStopped && Input.GetKey(KeyCode.F))
        {
            core.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}
