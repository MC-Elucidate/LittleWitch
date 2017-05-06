using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionTargetScript : MonoBehaviour {

    public Vector3 DistanceFromCharacter;

    void Start ()
    {
        ResetPosition();
	}

    void Update()
    {
    }

    public void ResetPosition()
    {
        transform.localPosition = DistanceFromCharacter;
    }

    public Vector3 GetTargetPosition()
    {
        return transform.position;
    }
}
