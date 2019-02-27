using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFinished : MonoBehaviour {

    public static LoadFinished Instance { get; private set; }
    public event Action Loaded;
    private void Awake()
    {
        Instance = this;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "stop")
        {
            if (Loaded != null)
                Loaded();
        }
    }
}
