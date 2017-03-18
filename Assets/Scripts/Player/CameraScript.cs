using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// #DESCRIPTION OF CLASS#
/// </summary>
public class CameraScript : MonoBehaviour
{
    #region Variables (private)

    // Inspector serialized	
    [SerializeField]
    private Transform cameraXform;
    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private PlayerMovementScript follow;
    [SerializeField]
    private Transform followXform;
    [SerializeField]
    private float widescreen = 0.2f;
    [SerializeField]
    private float targetingTime = 0.5f;
    [SerializeField]
    private float freeThreshold = -0.1f;


    // Smoothing and damping
    private Vector3 velocityCamSmooth = Vector3.zero;
    [SerializeField]
    private float camSmoothDampTime = 0.1f;
    private Vector3 velocityLookDir = Vector3.zero;
    [SerializeField]
    private float lookDirDampTime = 0.1f;


    // Private global only
    private Vector3 lookDir;
    private Vector3 curLookDir;
    private CamStates camState = CamStates.Behind;
    private Vector3 characterOffset;
    private Vector3 targetPosition;

    #endregion


    #region Properties (public)	

    public Transform CameraXform
    {
        get
        {
            return this.cameraXform;
        }
    }

    public Vector3 LookDir
    {
        get
        {
            return this.curLookDir;
        }
    }

    public CamStates CamState
    {
        get
        {
            return this.camState;
        }
    }

    public enum CamStates
    {
        Behind,         // Classic 3rd person camera
        Target,        // Used to centre camera behind character
    }

    public Vector3 RigToGoalDirection
    {
        get
        {
            // Move height and distance from character in separate parentRig transform since RotateAround has control of both position and rotation
            Vector3 rigToGoalDirection = Vector3.Normalize(characterOffset - this.transform.position);
            // Can't calculate distanceAway from a vector with Y axis rotation in it; zero it out
            rigToGoalDirection.y = 0f;

            return rigToGoalDirection;
        }
    }

    #endregion


    #region Unity event functions

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start()
    {
        cameraXform = this.transform;

        follow = GameObject.FindWithTag("Player").GetComponent<PlayerMovementScript>();
        followXform = GameObject.FindWithTag("Player").transform;

        lookDir = followXform.forward;
        curLookDir = followXform.forward;

        // Intialize values to avoid having 0s
        characterOffset = followXform.position + new Vector3(0f, distanceUp, 0f);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {

    }

    void LateUpdate()
    {
        float leftX = follow.sidewaysInput;
        float leftY = follow.forwardInput;

        characterOffset = followXform.position + (distanceUp * followXform.up);
        Vector3 lookAt = characterOffset;
        targetPosition = Vector3.zero;
        
        // Execute camera state
        switch (camState)
        {
            case CamStates.Behind:
                ResetCamera();

                // Only update camera look direction if moving
                if (follow.speed > follow.locomotionThreshold && follow.IsInLocomotion() && !follow.IsInPivot())
                {
                    lookDir = Vector3.Lerp(followXform.right * (leftX < 0 ? 1f : -1f), followXform.forward * (leftY < 0 ? -1f : 1f), Mathf.Abs(Vector3.Dot(this.transform.forward, followXform.forward)));
                    Debug.DrawRay(this.transform.position, lookDir, Color.white);

                    // Calculate direction from camera to player, kill Y, and normalize to give a valid direction with unit magnitude
                    curLookDir = Vector3.Normalize(characterOffset - this.transform.position);
                    curLookDir.y = 0;
                    Debug.DrawRay(this.transform.position, curLookDir, Color.green);

                    // Damping makes it so we don't update targetPosition while pivoting; camera shouldn't rotate around player
                    curLookDir = Vector3.SmoothDamp(curLookDir, lookDir, ref velocityLookDir, lookDirDampTime);
                }

                targetPosition = characterOffset + followXform.up * distanceUp - Vector3.Normalize(curLookDir) * distanceAway;
                Debug.DrawLine(followXform.position, targetPosition, Color.magenta);

                break;

            case CamStates.Target:
                ResetCamera();
                lookDir = followXform.forward;
                curLookDir = followXform.forward;

                targetPosition = characterOffset + followXform.up * distanceUp - lookDir * distanceAway;

                break;
            
        }

        SmoothPosition(cameraXform.position, targetPosition);
        transform.LookAt(lookAt);
    }

    #endregion


    #region Methods

    private void SmoothPosition(Vector3 fromPos, Vector3 toPos)
    {
        // Making a smooth transition between camera's current position and the position it wants to be in
        cameraXform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }

    /// <summary>
    /// Reset local position of camera inside of parentRig and resets character's look IK.
    /// </summary>
    private void ResetCamera()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime);
    }

    public void RecentrePressed()
    {
        camState = CamStates.Target;
    }
    public void RecentreReleased()
    {
        camState = CamStates.Behind;
    }

    #endregion Methods
}