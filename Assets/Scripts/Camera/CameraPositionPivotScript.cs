using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionPivotScript : MonoBehaviour {

    public float pitchInput = 0;
    private const float PITCH_MAX = 45;
    private const float PITCH_MIN = -10;
    private CameraPositionTargetScript cameraPositionTarget;
    // Use this for initialization
    void Start () {
        cameraPositionTarget = gameObject.GetComponentInChildren<CameraPositionTargetScript>();
	}
	
	// Update is called once per frame
	void Update () {

        if (pitchInput != 0)
        {
            Vector3 objRotation = transform.rotation.eulerAngles;
            float oldPitch = objRotation.x > 180 ? objRotation.x - 360 : objRotation.x;
            float newPitch = oldPitch + (pitchInput * Time.deltaTime);
            float clampedPitch = Mathf.Clamp(newPitch, PITCH_MIN, PITCH_MAX);
            transform.localEulerAngles = new Vector3(clampedPitch, 0, 0);
        }
    }

    public void ResetPosition()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
        cameraPositionTarget.ResetPosition();
    }
}
