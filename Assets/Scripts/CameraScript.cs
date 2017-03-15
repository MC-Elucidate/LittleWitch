using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float distanceAway = 5f;
    public float distanceUp = 1.5f;

    private Transform target;
    private Vector3 distanceFromTarget = new Vector3(0, 1.5f, 0);

    private Vector3 velocityCamSmooth = Vector3.zero;
    float camSmoothDampTime = 0.1f;

    // Use this for initialization
    void Start () {
        target = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 offset = target.position + distanceFromTarget;
        Vector3 lookDir = offset - transform.position;
        lookDir.y = 0;
        lookDir.Normalize();

        Vector3 destinationPosition = offset + (target.up * distanceUp) - (lookDir * distanceAway);

        smoothPosition(transform.position, destinationPosition);
        transform.LookAt(target.position);

	}

    private void smoothPosition(Vector3 fromPosition, Vector3 toPosition)
    {
        transform.position = Vector3.SmoothDamp(fromPosition, toPosition, ref velocityCamSmooth, camSmoothDampTime);
    }

}
