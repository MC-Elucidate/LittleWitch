using System;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    PlayerMovementScript playerMovement;
    MagicManager magicManager;
    CameraScript camera;
    CameraPositionPivotScript cameraPivot;

    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovementScript>();
        magicManager = gameObject.GetComponent<MagicManager>();
        camera = Camera.main.GetComponent<CameraScript>();
        cameraPivot = gameObject.GetComponentInChildren<CameraPositionPivotScript>();
    }

    void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        MovementInput();
        CameraInput();
        AimInput();
        SpellInput();
    }

    void MovementInput()
    {
        playerMovement.forwardInput = Input.GetAxis("Vertical");
        playerMovement.sidewaysInput = Input.GetAxis("Horizontal");
        
        if (Input.GetButtonDown("Jump"))
            playerMovement.Jump();
        if (Input.GetButtonUp("Jump"))
            playerMovement.EndJump();
    }

    void CameraInput()
    {
        float controllerInputVertical = Input.GetAxis("CameraVerticalC") * camera.controllerSensitivity;
        float controllerInputHorizontal = Input.GetAxis("CameraHorizontalC") * camera.controllerSensitivity;
        if (controllerInputHorizontal == 0)
        {
            camera.yawInput = Input.GetAxis("CameraHorizontalM") * camera.mouseSensitivity;
            playerMovement.yawInput = Input.GetAxis("CameraHorizontalM") * camera.mouseSensitivity;
        }
        else
        {
            camera.yawInput = controllerInputHorizontal;
            playerMovement.yawInput = controllerInputHorizontal;
        }

        if (controllerInputVertical == 0)
        {
            camera.pitchInput = Input.GetAxis("CameraVerticalM") * camera.mouseSensitivity;
            cameraPivot.pitchInput = Input.GetAxis("CameraVerticalM") * camera.mouseSensitivity;
        }
        else
        {
            camera.pitchInput = controllerInputVertical;
            cameraPivot.pitchInput = controllerInputVertical;
        }
    }

    void AimInput()
    {
        if (Input.GetButtonDown("Aim"))
        {
            cameraPivot.ResetPosition();
            magicManager.ClearInputs();
            camera.EnterAimMode();
            Debug.Log("Aiming!");
        }
        if (Input.GetButtonUp("Aim"))
        {
            magicManager.CastSpell();
            camera.LeaveAimMode();
            Debug.Log("Not Aiming anymore. Cast!");
        }
    }

    void SpellInput()
    {
        for (int i = 1; i <= 3; i++)
        {
            if (Input.GetButtonDown("Element" + i))
                magicManager.AddToInputString(i);
        }
    }

}
