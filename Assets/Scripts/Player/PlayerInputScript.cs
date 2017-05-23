using System;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    PlayerMovementScript playerMovement;
    MagicManager magicManager;
    UIManager uiManager;
    CameraScript camera;
    CameraPositionPivotScript cameraPivot;
    TimeSlow timeSlow;

    private enum TriggerState
    {
        Pressed = 1,
        Released = 2,
        Held = 3,
        NotHeld = 4
    }

    TriggerState RightTrigger;

    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovementScript>();
        magicManager = gameObject.GetComponent<MagicManager>();
        uiManager = gameObject.GetComponent<UIManager>();
        camera = Camera.main.GetComponent<CameraScript>();
        cameraPivot = gameObject.GetComponentInChildren<CameraPositionPivotScript>();
        timeSlow = gameObject.GetComponent<TimeSlow>();
        RightTrigger = TriggerState.NotHeld;
    }

    void Update()
    {
        CheckTriggers();
        HandleInputs();
        SaveTriggerStates();
    }

    private void HandleInputs()
    {
        MovementInput();
        CameraInput();
        AimInput();
        SpellInput();
        TimeInput();
    }

    private void CheckTriggers()
    {
        if (Input.GetAxisRaw("AimTrigger") == 1 && RightTrigger == TriggerState.NotHeld)
        {
            RightTrigger = TriggerState.Pressed;
        }
        else if (Input.GetAxisRaw("AimTrigger") == 0 && RightTrigger == TriggerState.Held)
        {
            RightTrigger = TriggerState.Released;
        }
    }

    private void SaveTriggerStates()
    {
        if (RightTrigger == TriggerState.Pressed)
            RightTrigger = TriggerState.Held;
        else if (RightTrigger == TriggerState.Released)
            RightTrigger = TriggerState.NotHeld;
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
        if (Input.GetButtonDown("Aim") || RightTrigger == TriggerState.Pressed)
        {
            cameraPivot.ResetPosition();
            magicManager.ClearInputs();
            camera.EnterAimMode();
            uiManager.ToggleCrosshair(true);
            Debug.Log("Aiming!");
        }
        if (Input.GetButtonUp("Aim") || RightTrigger == TriggerState.Released)
        {
            magicManager.CastSpell();
            camera.LeaveAimMode();
            uiManager.ToggleCrosshair(false);
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

    void TimeInput()
    {
        if (Input.GetButtonDown("TimeSlow"))
            timeSlow.SlowTime();
        if (Input.GetButtonUp("TimeSlow"))
            timeSlow.ResumeTime();
    }

}
