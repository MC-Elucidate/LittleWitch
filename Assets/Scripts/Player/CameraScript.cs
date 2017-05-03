using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public enum CameraMode
    {
        Free = 1,
        Aim = 2
    }

    public float yawInput = 0;
    public float pitchInput = 0;
    public float smooth = 0.2f;
    private float currentTurnAmount = 0f;
    private float turnSpeedVelocityChange = 0f;
    public CameraMode state { get; private set; }
    public float mouseSensitivity = 50f;
    public float controllerSensitivity = 180f;

    private Transform lookTarget;
    private Transform positionTarget;
    public Vector3 distanceFromTarget = new Vector3(0f, 0f, -4f);

    private const float PITCH_MAX_ANGLE = 45.0f;
    private const float PITCH_MIN_ANGLE = 0.0f;
    
    void Start()
    {
        lookTarget = GameObject.FindGameObjectWithTag("CameraLookTarget").transform;
        positionTarget = GameObject.FindGameObjectWithTag("CameraPositionTarget").transform;
        state = CameraMode.Free;
    }

    void LateUpdate()
    {
        if (state == CameraMode.Free)
        {
            if (yawInput != 0)
                transform.RotateAround(lookTarget.position, Vector3.up, Time.deltaTime * yawInput);

            if (pitchInput != 0)
            {
                Vector3 objRotation = transform.rotation.eulerAngles;
                float newPitch = objRotation.x + (pitchInput * Time.deltaTime);
                float clampedY = Mathf.Clamp(newPitch, PITCH_MIN_ANGLE, PITCH_MAX_ANGLE);
                transform.localEulerAngles = new Vector3(clampedY, objRotation.y, objRotation.z);
            }
            transform.position = Vector3.Lerp(transform.position, lookTarget.position + transform.rotation * distanceFromTarget, smooth);
        }
        else if (state == CameraMode.Aim)
        {
            transform.position = Vector3.Lerp(transform.position, positionTarget.position, 0.5f);
        }
        transform.LookAt(lookTarget.position);
    }

    public void EnterAimMode() { state = CameraMode.Aim; }

    public void LeaveAimMode() { state = CameraMode.Free; }
}