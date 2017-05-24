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
    public float freeSmooth = 0.2f;
    public float aimSmooth = 0.5f;
    public CameraMode state { get; private set; }
    public float mouseSensitivity = 50f;
    public float controllerSensitivity = 180f;
    private float timeEffect = 1;

    private Transform lookTarget;
    private CameraPositionPivotScript pivotTarget;
    public Vector3 distanceFromTarget = new Vector3(0f, 0f, -4f);

    private const float PITCH_MAX_ANGLE = 45.0f;
    private const float PITCH_MIN_ANGLE = -10.0f;
    
    void Start()
    {
        lookTarget = GameObject.FindGameObjectWithTag(Helpers.Tags.CameraFollowTarget).transform;
        pivotTarget = GameObject.FindGameObjectWithTag(Helpers.Tags.CameraPositionPivot).GetComponent<CameraPositionPivotScript>();
        state = CameraMode.Free;
    }

    void LateUpdate()
    {
        if (state == CameraMode.Free)
        {
            if (yawInput != 0)
                transform.RotateAround(lookTarget.position, Vector3.up, DeltaTime * yawInput);

            if (pitchInput != 0)
            {
                Vector3 objRotation = transform.rotation.eulerAngles;
                float oldPitch = objRotation.x > 180 ? objRotation.x - 360 : objRotation.x;
                float newPitch = oldPitch + (pitchInput * DeltaTime);
                float clampedPitch = Mathf.Clamp(newPitch, PITCH_MIN_ANGLE, PITCH_MAX_ANGLE);
                transform.localEulerAngles = new Vector3(clampedPitch, objRotation.y, objRotation.z);
            }
            transform.position = Vector3.Lerp(transform.position, lookTarget.position + transform.rotation * distanceFromTarget, aimSmooth);
        }
        else if (state == CameraMode.Aim)
        {
            transform.position = Vector3.Lerp(transform.position, pivotTarget.GetTagetPosition(), aimSmooth);
        }
        transform.LookAt(lookTarget.position);
    }

    public void EnterAimMode() { state = CameraMode.Aim; }

    public void LeaveAimMode() { state = CameraMode.Free; }

    private float DeltaTime
    { get { return Time.deltaTime / timeEffect; } }

    public void TimeSlowMovementActive(float TimeSlowMultiplier)
    {
        this.timeEffect = TimeSlowMultiplier;
    }

    public void TimeSlowMovementDeactive()
    {
        this.timeEffect = 1;
    }
}