using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float xValue = 0;
    public float yValue = 0;
    public float smooth;

    private Transform target;
    private Vector3 distanceFromTarget;

    private const float Y_MAX_ANGLE = 45.0f;
    private const float Y_MIN_ANGLE = 0.0f;

    // Use this for initialization
    void Start () {
        target = transform.parent;
        distanceFromTarget = new Vector3(0f, 0f, -5f);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        yValue = Mathf.Clamp(yValue, Y_MIN_ANGLE, Y_MAX_ANGLE);
        Quaternion rotation = Quaternion.Euler(yValue, xValue, 0);
        transform.position = target.position + rotation * distanceFromTarget;
        transform.LookAt(target.position);
	}

}
