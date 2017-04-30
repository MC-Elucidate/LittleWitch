using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{


    public float pitchValue = 0;
    public float yawValue = 0;
    public float smooth = 0.2f;
    private float currentTurnAmount = 0f;
    private float turnSpeedVelocityChange = 0f;
    public float mouseSensitivity = 50f;
    public float controllerSensitivity = 180f;

    private Transform target;
    public Vector3 distanceFromTarget = new Vector3(0f, 0f, -4f);

    private const float PITCH_MAX_ANGLE = 45.0f;
    private const float PITCH_MIN_ANGLE = 10.0f;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("CameraLookTarget").transform;
    }

    void LateUpdate()
    {
        if (pitchValue != 0)
            transform.RotateAround(target.position, Vector3.up, Time.deltaTime * pitchValue);
        if (yawValue != 0)
        {
            Vector3 objRotation = transform.rotation.eulerAngles;
            float newPitch = objRotation.x + (yawValue * Time.deltaTime);
            float clampedY = Mathf.Clamp(newPitch, PITCH_MIN_ANGLE, PITCH_MAX_ANGLE);
            transform.localEulerAngles = new Vector3(clampedY, objRotation.y, objRotation.z);
        }
        transform.position = Vector3.Lerp(transform.position, target.position + transform.rotation * distanceFromTarget, smooth);
        transform.LookAt(target.position);
    }
}