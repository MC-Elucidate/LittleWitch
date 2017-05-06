using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionPivotScript : MonoBehaviour {

    public float pitchInput = 0;
    public float pitchMaxAngle = 45;
    public float pitchMinAngle = -10;
    private CameraPositionTargetScript cameraPositionTarget;

    void Start () {
        cameraPositionTarget = gameObject.GetComponentInChildren<CameraPositionTargetScript>();
	}

	void Update () {

        if (pitchInput != 0)
        {
            Vector3 objRotation = transform.rotation.eulerAngles;
            float oldPitch = objRotation.x > 180 ? objRotation.x - 360 : objRotation.x;
            float newPitch = oldPitch + (pitchInput * Time.deltaTime);
            float clampedPitch = Mathf.Clamp(newPitch, pitchMinAngle, pitchMaxAngle);
            transform.localEulerAngles = new Vector3(clampedPitch, 0, 0);
        }
    }

    public void ResetPosition()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
        cameraPositionTarget.ResetPosition();
    }

    public Vector3 GetTagetPosition()
    {
        return cameraPositionTarget.GetTargetPosition();
    }
}
