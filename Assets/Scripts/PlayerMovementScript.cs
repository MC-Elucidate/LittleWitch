using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

    private Animator animator;
    private CharacterController characterController;
    private float directionDampTime = 0.25f;

    public float sidewaysInput;
    public float forwardInput;
    public float directionSpeed = 3f;
    public bool isGrounded;
    public LayerMask platformsLayer;
    public Transform cameraTransform;

    private bool jump = false;


    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        characterController = gameObject.GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGrounded();


        Vector3 rootDirection = transform.forward;
        Vector3 inputDirection = new Vector3(sidewaysInput, 0, forwardInput);
        Vector3 cameraDirection = cameraTransform.forward;
        cameraDirection.y = 0;
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);
        Vector3 moveDirection = referentialShift * inputDirection;
        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);
        float speed = inputDirection.sqrMagnitude;
        float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1 : 1);
        angleRootToMove /= 180f;
        float direction = angleRootToMove * directionSpeed;

        animator.SetFloat("Speed", speed);
        animator.SetFloat("Direction", direction, directionDampTime, Time.deltaTime);

    }

    void FixedUpdate()
    {
    }

    public void Jump()
    { 
    }

    private void CheckIsGrounded()
    {
        if (Physics.Raycast(transform.position, -transform.up, 0.1f, platformsLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
