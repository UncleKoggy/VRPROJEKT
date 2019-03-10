using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Fake : MonoBehaviour {
    public Grenade grenade;

    public Transform kolowrotek, loadCube;
    public Rigidbody core;

    public Quaternion startRot;
    public Vector3 startPos;

    private bool canShoot = false;
    // Use this for initialization
    private void Awake()
    {
        startRot = kolowrotek.rotation;
        startPos = loadCube.position;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float angle = kolowrotek.rotation.eulerAngles.z;
        angle = angle / 80;

        //Debug.Log(angle);
        if(angle >= 3.5f)
        {
            canShoot = true;
        }
        if (!canShoot)
        {
            loadCube.position = new Vector3(loadCube.position.x, loadCube.position.y, -angle);
        }
    }
    public void Shoot()
    {
        if (canShoot)
        {
            //loadCube.position = startPos;
            //core.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            kolowrotek.rotation = startRot;
            canShoot = false;
            //grenade.Explode();
        }
    }
}
