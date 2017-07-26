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
    private bool backToCharacter = false;
    private float yRangeLimit = 3.5f;

    private Transform lookTarget;
    private CameraPositionPivotScript pivotTarget;
    private PlayerStatus playerStatus;
    private PlayerMovementScript playerMovement;
    public Vector3 distanceFromTarget = new Vector3(0f, 0f, -4f);

    private const float PITCH_MAX_ANGLE = 45.0f;
    private const float PITCH_MIN_ANGLE = -10.0f;
    
    void Start()
    {
        lookTarget = GameObject.FindGameObjectWithTag(Helpers.Tags.CameraFollowTarget).transform;
        pivotTarget = GameObject.FindGameObjectWithTag(Helpers.Tags.CameraPositionPivot).GetComponent<CameraPositionPivotScript>();
        playerStatus = GameObject.FindGameObjectWithTag(Helpers.Tags.Player).GetComponent<PlayerStatus>();
        playerMovement = GameObject.FindGameObjectWithTag(Helpers.Tags.Player).GetComponent<PlayerMovementScript>();
        state = CameraMode.Free;
        transform.position = lookTarget.position + transform.rotation * distanceFromTarget;
    }

    void LateUpdate()
    {
        if (state == CameraMode.Free)
        {
            if (yawInput != 0)
                transform.RotateAround(lookTarget.position, Vector3.up, DeltaTime * yawInput);
            
            Vector3 lerpDestination = lookTarget.position;
            bool cameraInYRange = Mathf.Abs(lerpDestination.y - transform.position.y) < yRangeLimit;
            bool playerInAir = (playerMovement.velocity.y > 0 || (playerMovement.velocity.y < 0 && !playerMovement.isGrounded));

            if (playerInAir)
            {
                if (cameraInYRange && !backToCharacter)
                {
                    lerpDestination.y = transform.position.y;
                }
                else
                    backToCharacter = true;
            }
            else if(Mathf.Abs(lookTarget.position.y - transform.position.y) < 0.15f)
            {
                backToCharacter = false;
            }

            Quaternion lerpAngle = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
            transform.position = Vector3.Lerp(transform.position, lerpDestination + lerpAngle * distanceFromTarget, 0.2f);

            Quaternion currentRot = transform.rotation;
            transform.LookAt(lerpDestination);
            Quaternion destinationRot = transform.rotation;
            transform.rotation = Quaternion.Lerp(currentRot, destinationRot, 0.2f);


        }
        else if (state == CameraMode.Aim)
        {
            transform.position = Vector3.Lerp(transform.position, pivotTarget.GetTagetPosition(), aimSmooth);
            transform.LookAt(pivotTarget.transform.position);
        }
    }

    private void EnterAimMode() { if(state != CameraMode.Aim) state = CameraMode.Aim; }

    private void LeaveAimMode() { if (state != CameraMode.Free) state = CameraMode.Free; }

    public void SetCameraState()
    {
        if (playerStatus.state == PlayerStatus.PlayerState.Aiming)
            EnterAimMode();
        else if (playerStatus.state == PlayerStatus.PlayerState.FreeMovement)
            LeaveAimMode();
    }

    private float DeltaTime
    { get { return Time.deltaTime / timeEffect; } }

    public void TimeSlowMovementActive(float TimeSlowMultiplier)
    {
        this.timeEffect = TimeSlowMultiplier;
        pivotTarget.SetTimeEffect(TimeSlowMultiplier);
    }

    public void TimeSlowMovementDeactive()
    {
        this.timeEffect = 1;
        pivotTarget.SetTimeEffect(1);
    }
}