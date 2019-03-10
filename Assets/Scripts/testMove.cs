using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMove : MonoBehaviour {

    private Vector3 currentPosition;
    private Vector3 wantedPosition;
    public Vector3 offset;
    private Rigidbody rb;
    private Transform selfTr;
    public Transform kolowrotek;
    private Quaternion quaternion;
    public float minAngle = 0f;
    public float maxAngle = 720f;
    public float currentAngle;
    public float lerp = 0f;
    public float angle = 0.0f;
    public Vector3 axis = Vector3.zero;

    void Start () {
        rb = GetComponent<Rigidbody>();
        selfTr = GetComponent<Transform>();
        currentPosition = selfTr.position;
        wantedPosition = currentPosition + offset;
        quaternion = kolowrotek.rotation;
        
        
        quaternion.ToAngleAxis(out angle, out axis);
	}
	
	void FixedUpdate () {        
        float parametr = Mathf.InverseLerp(minAngle, maxAngle, currentAngle);
        Debug.Log("current angle: "+currentAngle);
        Debug.Log(quaternion);
        Debug.Log(angle);
        Debug.Log(axis);

        //Debug.Log("parametr: " + parametr);
    }
}
