using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// #DESCRIPTION OF CLASS#
/// </summary>
public class CameraScript : MonoBehaviour
{
    protected Transform target;
    protected PlayerMovementScript targetMovement;
    protected Transform cameraTransform; // the transform of the camera

    public float recentreTurnSpeed = 10f;
    public float regularTurnSpeed = 1f;

    private float moveSpeed; // How fast the rig will move to keep up with target's position
    private float cameraRotateSpeed; // How fast the rig will turn to keep up with target's rotation
    private float rollSpeed = 0.2f;// How fast the rig will roll (around Z axis) to match target's roll.
    private bool followVelocity = true;// Whether the rig will rotate in the direction of the target's velocity.
    private bool FollowTilt = false; // Whether the rig will tilt (around X axis) with the target. (Usable on slopes?)
    private bool recentre = false;
    private float smoothTurnTime = 0.2f; // the smoothing for the camera's rotation
    
    private float currentTurnAmount; // How much to turn the camera
    private float turnSpeedVelocityChange; // The change in the turn speed velocity
    private Vector3 rollUp = Vector3.up;// The roll of the camera around the z axis ( generally this will always just be up )

    protected virtual void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        targetMovement = player.GetComponent<PlayerMovementScript>();
        cameraTransform = GetComponentInChildren<Camera>().transform;

        moveSpeed = targetMovement.movespeed;
        cameraRotateSpeed = regularTurnSpeed;
    }

    private void LateUpdate()
    {
        FollowTarget(Time.deltaTime);
    }

    protected void FollowTarget(float deltaTime)
    {
        // if no target, or no time passed then we quit early, as there is nothing to do
        if (!(deltaTime > 0) || target == null)
        {
            return;
        }

        // initialise some vars, we'll be modifying these in a moment
        var targetForward = target.forward;
        var targetUp = target.up;

        if (followVelocity && Application.isPlaying)
        {
            currentTurnAmount = Mathf.SmoothDamp(currentTurnAmount, 1, ref turnSpeedVelocityChange, smoothTurnTime);
        }

        if (recentre)
            cameraRotateSpeed = recentreTurnSpeed;
        else
            cameraRotateSpeed = regularTurnSpeed;

        // camera position moves towards target position:
        transform.position = Vector3.Lerp(transform.position, target.position, deltaTime * moveSpeed);

        // camera's rotation is split into two parts, which can have independend speed settings:
        // rotating towards the target's forward direction (which encompasses its 'yaw' and 'pitch')
        if (!FollowTilt)
        {
            targetForward.y = 0;
            if (targetForward.sqrMagnitude < float.Epsilon)
            {
                targetForward = transform.forward;
            }
        }
        var rollRotation = Quaternion.LookRotation(targetForward, rollUp);

        // and aligning with the target object's up direction (i.e. its 'roll')
        rollUp = rollSpeed > 0 ? Vector3.Slerp(rollUp, targetUp, rollSpeed * deltaTime) : Vector3.up;

        if (targetMovement.sidewaysInput != 0 || recentre)
            transform.rotation = Quaternion.Lerp(transform.rotation, rollRotation, cameraRotateSpeed * currentTurnAmount * deltaTime);
    }

    public void RecentrePressed() { recentre = true; }
    public void RecentreReleased() { recentre = false; }
}