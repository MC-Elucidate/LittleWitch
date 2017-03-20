using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

    //Private GameObjects
    private Animator animator;
    private CharacterController characterController;
    public Transform cameraTransform;

    //Movement details
    private float directionDampTime = 0.25f;
    private float speedDampTime = 0.05f;
    private float rotationDegreesPerSecond = 120f;
    private float direction;
    private float pivotAngle;
    public Vector3 velocity;

    //Public variables
    public float sidewaysInput;
    public float forwardInput;
    public float directionSpeed = 3f;
    public float speed = 0f;
    public float locomotionThreshold = 0.7f;
    public float movespeed = 10f;
    public float gravity = -10f;
    public float rotationDampSpeed = 0.3f;
    public float jumpPower = 10f;
    public bool jump = false;
    public bool isGrounded;
    public LayerMask platformsLayer;

    //Hashes
    private int locomotionHashID;
    private int pivotLeftHashID;
    private int pivotRightHashID;
    private int idlePivotLeftHashID;
    private int idlePivotRightHashID;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        characterController = gameObject.GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        locomotionHashID = Animator.StringToHash("Base Layer.Locomotion");
        pivotLeftHashID = Animator.StringToHash("Base Layer.LocomotionPivotLeft");
        pivotRightHashID = Animator.StringToHash("Base Layer.LocomotionPivotRight");
        idlePivotLeftHashID = Animator.StringToHash("Base Layer.IdlePivotLeft");
        idlePivotRightHashID = Animator.StringToHash("Base Layer.IdlePivotRight");
    }
    
    void Update()
    {
        CheckIsGrounded();

        //Calculate Movement Values
        //Vector3 rootDirection = transform.forward;
        //Vector3 inputDirection = new Vector3(sidewaysInput, 0, forwardInput);
        //Vector3 cameraDirection = cameraTransform.forward;
        //cameraDirection.y = 0;
        //Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);
        //Vector3 moveDirection = referentialShift * inputDirection;
        //Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);
        //speed = inputDirection.sqrMagnitude;
        //float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1 : 1);

        //if(!IsInPivot())
        //    pivotAngle = angleRootToMove;

        //angleRootToMove /= 180f;
        //direction = angleRootToMove * directionSpeed * (speed == 0 ? 0 : 1);

        Vector3 directionToMove = Quaternion.LookRotation(Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up), Vector3.up) * new Vector3(sidewaysInput, 0, forwardInput) * movespeed;
        directionToMove = Vector3.ClampMagnitude(directionToMove, movespeed);


        float speed = new Vector2(sidewaysInput, forwardInput).sqrMagnitude;
        animator.SetFloat("Speed", speed);
        //animator.SetFloat("Direction", 0, directionDampTime, Time.deltaTime);

        if (directionToMove.magnitude != 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionToMove.normalized), rotationDampSpeed);


        velocity.x = directionToMove.x;
        velocity.z = directionToMove.z;

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        if (jump)
        {
            velocity.y = jumpPower;
            jump = false;
        }

        characterController.Move(velocity * Time.deltaTime);

        //Set Animator Values
        //animator.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);

        /*
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
    }

    void FixedUpdate()
    {
    }

    public void Jump()
    {
        if(isGrounded)
            jump = true;
    }

    private void CheckIsGrounded()
    {
        if (Physics.Raycast(transform.position, -transform.up, 0.1f, platformsLayer))
        {
            isGrounded = true;
            velocity.y = 0;
        }
        else
        {
            isGrounded = false;
        }
    }

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
}
