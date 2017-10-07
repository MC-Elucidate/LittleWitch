using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    //GameObjects
    private Animator animator;
    private CharacterController characterController;
    private PlayerStatus playerStatus;
    private PlayerSoundManager sounds;
    public Transform cameraTransform;

    //Movement variables
    public Vector3 velocity;
    public float sidewaysInput;
    public float forwardInput;
    public float movespeed = 10f;
    public float gravity = -10f;
    public float rotationDampSpeed = 0.5f;
    public float airMovementAcceleration = 0.8f;
    private float timeEffect = 1;
    public bool isGrounded;
    public LayerMask platformsLayer;

    //private float directionDampTime = 0.25f;
    //private float speedDampTime = 0.05f;
    //private float rotationDegreesPerSecond = 120f;
    //private float direction;
    //private float pivotAngle;
    //public float directionSpeed = 3f;
    //public float speed = 0f;
    //public float locomotionThreshold = 0.7f;


    //Jump variables
    public bool jumping = false;
    public float jumpPower = 4f;
    public float jumpHoldMax = 0.35f;
    public float jumpLeniencyTime = 0.25f;
    private float airTime = 0;
    private float jumpHoldCurrent = 0f;

    //Camera variables
    public float yawInput;

    //Hashes
    private int locomotionHashID;
    private int pivotLeftHashID;
    private int pivotRightHashID;
    private int idlePivotLeftHashID;
    private int idlePivotRightHashID;

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        characterController = gameObject.GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        playerStatus = gameObject.GetComponent<PlayerStatus>();
        sounds = gameObject.GetComponent<PlayerSoundManager>();
        locomotionHashID = Animator.StringToHash("Base Layer.Locomotion");
        pivotLeftHashID = Animator.StringToHash("Base Layer.LocomotionPivotLeft");
        pivotRightHashID = Animator.StringToHash("Base Layer.LocomotionPivotRight");
        idlePivotLeftHashID = Animator.StringToHash("Base Layer.IdlePivotLeft");
        idlePivotRightHashID = Animator.StringToHash("Base Layer.IdlePivotRight");
    }

    void Update()
    {
        CheckIsGrounded();
        CheckJumpHoldTimer();
        UpdateMovement();
        AimRotation();
        UpdateAirTime();
        SetAnimatorValues();
    }

    void UpdateMovement()
    {
        Vector3 movementVector;
        if (playerStatus.state == PlayerStatus.PlayerState.Aiming)
            movementVector = new Vector3(0, 0, 0);

        else
        {
            Quaternion directionToMove = Quaternion.LookRotation(Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up), Vector3.up);
            movementVector = directionToMove * new Vector3(sidewaysInput, 0, forwardInput) * movespeed;
            movementVector = Vector3.ClampMagnitude(movementVector, movespeed);
        }
        //if (!isGrounded) //Apply slippery movement
        //{
        //    Vector3 aerialInput = new Vector3(movementVector.x, 0, movementVector.z) * airMovementAcceleration * Time.deltaTime;
        //    Vector3 clampedMoveSpeed = Vector3.ClampMagnitude(new Vector3(velocity.x + aerialInput.x, 0, velocity.z + aerialInput.z), movespeed);
        //    velocity.x = clampedMoveSpeed.x;
        //    velocity.z = clampedMoveSpeed.z;

        //if (isGrounded)
        //    velocity.y = gravity;
        //else
        //    velocity.y += gravity * Time.deltaTime;
        //}
        //else //Apply regular movement
        //{
            velocity.x = movementVector.x;
            velocity.z = movementVector.z;
            if (!jumping)
            {
                if(isGrounded)
                    velocity.y = gravity;
                else
                    velocity.y += gravity * DeltaTime;
            }
        //}
        if (movementVector.magnitude != 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementVector.normalized), rotationDampSpeed);

        characterController.Move(velocity * DeltaTime);
    }

    private void AimRotation()
    {
        if (playerStatus.state != PlayerStatus.PlayerState.Aiming)
            return;

        if (yawInput != 0)
            transform.Rotate(new Vector3(0, (Time.deltaTime/Time.timeScale) * yawInput, 0));
    }

    #region Jump
    public void Jump()
    {
        bool jumpIsWithinLeniencyWindow = !isGrounded && airTime <= jumpLeniencyTime;

        if (isGrounded || jumpIsWithinLeniencyWindow)
        {
            jumping = true;
            velocity.y = jumpPower;
            sounds.PlayJumpSound();

            if(jumpIsWithinLeniencyWindow)
                print("Saved by the leniency");
        }
    }

    public void SteppedOnJumpPad(float jumpPadPower)
    {
        jumping = true;
        velocity.y = jumpPadPower;
    }

    public void EndJump()
    {
        jumping = false;
        jumpHoldCurrent = 0f;
    }



    private void CheckJumpHoldTimer()
    {
        if (!jumping)
            return;

        jumpHoldCurrent += DeltaTime;

        if (jumpHoldCurrent >= jumpHoldMax)
            EndJump();
    }

    private void UpdateAirTime()
    {
        if (!isGrounded)
            airTime += Time.deltaTime;
        else
            airTime = 0;
    }
    #endregion

    #region GroundDetection
    private void CheckIsGrounded()
    {
        bool previouslyGrounded = isGrounded;
        //get the radius of the players capsule collider, and make it a tiny bit smaller than that
        float radius = characterController.radius * 0.9f;
        //get the position (assuming its right at the bottom) and move it up by almost the whole radius
        Vector3 pos = transform.position + Vector3.up * 2 * (radius * 0.9f);
        //returns true if the sphere touches something on that layer
        RaycastHit raycastHitInfo;
        isGrounded = Physics.SphereCast(pos, radius, Vector3.down, out raycastHitInfo, radius, platformsLayer);

        if (isGrounded)
            ParentToObject(raycastHitInfo.transform);
        else
            transform.parent = null;
            

        if (previouslyGrounded && !isGrounded && !jumping)
            velocity.y = 0;
        if (!previouslyGrounded && isGrounded)
            EndJump();
        
    }

    private void ParentToObject(Transform otherObject)
    {
        if (otherObject.root.tag == Helpers.Tags.MovingPlatform)
        {
            this.transform.parent = otherObject;
            try
            {
                otherObject.root.GetComponentInChildren<Platform>().StoodOn();
            }
            catch (System.Exception)
            {
                Debug.Log("Something went wrong calling StoodOn()");
            }
        }
        else
            this.transform.parent = null;
    }
    #endregion

    #region Animation
    public bool IsInLocomotion()
    {
        return animator.GetCurrentAnimatorStateInfo(0).fullPathHash == locomotionHashID;
    }
    public bool IsInPivot()
    {
        return animator.GetCurrentAnimatorStateInfo(0).fullPathHash == pivotLeftHashID ||
            animator.GetCurrentAnimatorStateInfo(0).fullPathHash == pivotRightHashID ||
            animator.GetCurrentAnimatorStateInfo(0).fullPathHash == idlePivotRightHashID ||
            animator.GetCurrentAnimatorStateInfo(0).fullPathHash == idlePivotLeftHashID;
    }
    private void SetAnimatorValues()
    {
        float speed = new Vector2(velocity.x, velocity.z).sqrMagnitude;
        animator.SetFloat("Speed", speed);
        animator.SetBool("InAir", !isGrounded);
    }
    #endregion

    #region TimeSlow
    private float DeltaTime
    { get { return Time.deltaTime / timeEffect; } }

    public void TimeSlowMovementActive(float TimeSlowMultiplier)
    {
        this.timeEffect = TimeSlowMultiplier;
        animator.speed = animator.speed / TimeSlowMultiplier;
    }

    public void TimeSlowMovementDeactive()
    {
        this.timeEffect = 1;
        animator.speed = 1;
    }
    #endregion
}


//Stuff needed for pivot animations later
/*
    Calculate Movement Values
    Vector3 rootDirection = transform.forward;
    Vector3 inputDirection = new Vector3(sidewaysInput, 0, forwardInput);
    Vector3 cameraDirection = cameraTransform.forward;
    cameraDirection.y = 0;
    Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);
    Vector3 moveDirection = referentialShift * inputDirection;
    Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);
    speed = inputDirection.sqrMagnitude;
    float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1 : 1);

    if(!IsInPivot())
        pivotAngle = angleRootToMove;

    angleRootToMove /= 180f;
    direction = angleRootToMove* directionSpeed * (speed == 0 ? 0 : 1);



    Set Animator Values
    animator.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);

    
    if (speed > locomotionThreshold)
    {
        if (!IsInPivot())
        {
            animator.SetFloat("Angle", pivotAngle);
            animator.SetFloat("Direction", direction, directionDampTime, Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Direction", 0);
        }
    }
    if (speed < locomotionThreshold && Mathf.Abs(sidewaysInput) < 0.05f)
    {
        animator.SetFloat("Angle", 0);
        animator.SetFloat("Direction", 0);
    }
    */
