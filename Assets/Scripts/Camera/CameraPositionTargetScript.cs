using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionTargetScript : MonoBehaviour {

    public Vector3 DistanceFromCharacter;
    public float pitchInput = 0;
    private Transform parent;
    private Transform lookTarget;
    
	void Start ()
    {
        parent = gameObject.GetComponentInParent<Transform>();
        lookTarget = GameObject.FindGameObjectWithTag("CameraLookTarget").transform;
	}

    void Update()
    {
        if (pitchInput != 0)
            transform.RotateAround(lookTarget.position, parent.right, Time.deltaTime * pitchInput);
    }

    public void ResetPosition()
    {
        transform.localPosition = DistanceFromCharacter;
    }
}
