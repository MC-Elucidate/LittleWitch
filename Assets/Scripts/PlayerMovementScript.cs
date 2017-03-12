using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {
    
    private Animator animator;
    private CharacterController characterController;
    private float directionDampTime = 0.25f;

    public float sidewaysInput;
    public float forwardInput;
    public Vector3 velocity;
    public float movespeed = 10f;
    public float jumpPower = 10f;
    public float gravity = -10f;
    public bool isGrounded;
    public LayerMask platformsLayer;
    public Transform cameraTransform;

    private bool jump = false;


    // Use this for initialization
    void Start ()
    {
        velocity = new Vector3(0, 0, 0);
        animator = gameObject.GetComponent<Animator>();
        characterController = gameObject.GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckIsGrounded();

        
        Vector3 directionToMove = Quaternion.LookRotation(Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up), Vector3.up) * new Vector3(sidewaysInput, 0, forwardInput) * movespeed;
        directionToMove = Vector3.ClampMagnitude(directionToMove, movespeed);


        float speed = new Vector2(sidewaysInput, forwardInput).sqrMagnitude;
        animator.SetFloat("Speed", speed);
        animator.SetFloat("Direction", 0, directionDampTime, Time.deltaTime);

        if (directionToMove.magnitude != 0)
            transform.rotation = Quaternion.LookRotation(directionToMove.normalized);


        velocity.x = directionToMove.x;
        velocity.z = directionToMove.z;

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        if (jump)
        {
            velocity.y += jumpPower;
            jump = false;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
    }

    public void Jump()
    {
        if (isGrounded)
        {
            jump = true;
        }
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
}
