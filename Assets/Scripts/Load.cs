using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{

    public Transform kolowrotek,core;
    public Rigidbody rb;

    float lAngle = 0;
    public float mltp = 0.2f;
    public float pow = 10f;
    public bool canLoad = true;
    public bool canShoot = false;
    public Vector3 offset = new Vector3(0f,0f, -1f);
    private void Start()
    {
        LoadFinished.Instance.Loaded += Stop;
    }

    private void Stop()
    {
        canShoot = true;
        canLoad = false;
    }

    void Update()
    {
        if (canLoad)
        {
            Quaternion a = kolowrotek.rotation;

            float angle = kolowrotek.rotation.eulerAngles.z;

            core.transform.rotation *= Quaternion.AngleAxis((angle - lAngle) * mltp, core.transform.right);

            lAngle = angle;
        }
        if (canShoot && Input.GetKeyDown(KeyCode.Space))
        {
            rb.constraints = RigidbodyConstraints.None;
            Debug.Log("strzał");
            //rb.AddTorque(new Vector3(pow, 0f, 0f), ForceMode.Impulse);

        }
    }
}
