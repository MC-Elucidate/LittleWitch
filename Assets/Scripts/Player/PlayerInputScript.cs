using System;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    PlayerMovementScript playerMovement;
    MagicManager magicManager;
    CameraScript camera;

    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovementScript>();
        magicManager = gameObject.GetComponent<MagicManager>();
        camera = Camera.main.GetComponent<CameraScript>();
    }

    void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        //Movement
        playerMovement.forwardInput = Input.GetAxis("Vertical");
        playerMovement.sidewaysInput = Input.GetAxis("Horizontal");
        
        //Jump
        if (Input.GetButtonDown("Jump"))
            playerMovement.Jump();
        if (Input.GetButtonUp("Jump"))
            playerMovement.EndJump();

        //Camera
        float controllerInputVertical = Input.GetAxis("CameraVerticalC") * camera.controllerSensitivity;
        float controllerInputHorizontal = Input.GetAxis("CameraHorizontalC") * camera.controllerSensitivity;
        if (controllerInputHorizontal == 0)
            camera.pitchValue = Input.GetAxis("CameraHorizontalM") * camera.mouseSensitivity;
        else
            camera.pitchValue = controllerInputHorizontal;

        if (controllerInputVertical == 0)
            camera.yawValue = Input.GetAxis("CameraVerticalM") * camera.mouseSensitivity;
        else
            camera.yawValue = controllerInputVertical;

        //Aim
        if (Input.GetButtonDown("Aim"))
        {
            magicManager.ClearInputs();
            Debug.Log("Aiming!");
        }
        if (Input.GetButtonUp("Aim"))
        {
            Debug.Log("Not Aiming anymore. Cast!");
            magicManager.CastSpell();
        }

        for (int i = 1; i <= 3; i++)
        {
            if (Input.GetButtonDown("Element" + i))
                magicManager.AddToInputString(i);
        }
    }
}
