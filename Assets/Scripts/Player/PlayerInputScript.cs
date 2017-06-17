using System;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    PlayerMovementScript playerMovement;
    MagicManager magicManager;
    UIManager uiManager;
    PlayerStatus playerStatus;
    CameraScript cameraScript;
    CameraPositionPivotScript cameraPivot;
    TimeSlowManager timeSlow;
    LockOnManager lockOn;

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
        cameraScript = Camera.main.GetComponent<CameraScript>();
        cameraPivot = gameObject.GetComponentInChildren<CameraPositionPivotScript>();
        timeSlow = gameObject.GetComponent<TimeSlowManager>();
        playerStatus = gameObject.GetComponent<PlayerStatus>();
        lockOn = gameObject.GetComponent<LockOnManager>();
        RightTrigger = TriggerState.NotHeld;
        HideCursor();
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
        LockOnInput();
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
        float controllerInputVertical = Input.GetAxis("CameraVerticalC") * cameraScript.controllerSensitivity;
        float controllerInputHorizontal = Input.GetAxis("CameraHorizontalC") * cameraScript.controllerSensitivity;
        if (controllerInputHorizontal == 0)
        {
            cameraScript.yawInput = Input.GetAxis("CameraHorizontalM") * cameraScript.mouseSensitivity;
            playerMovement.yawInput = Input.GetAxis("CameraHorizontalM") * cameraScript.mouseSensitivity;
        }
        else
        {
            cameraScript.yawInput = controllerInputHorizontal;
            playerMovement.yawInput = controllerInputHorizontal;
        }

        if (controllerInputVertical == 0)
        {
            cameraScript.pitchInput = Input.GetAxis("CameraVerticalM") * cameraScript.mouseSensitivity;
            cameraPivot.pitchInput = Input.GetAxis("CameraVerticalM") * cameraScript.mouseSensitivity;
        }
        else
        {
            cameraScript.pitchInput = controllerInputVertical;
            cameraPivot.pitchInput = controllerInputVertical;
        }
    }

    void AimInput()
    {
        if (Input.GetButtonDown("Aim") || RightTrigger == TriggerState.Pressed)
        {
            cameraPivot.ResetPosition();
            magicManager.ClearInputs();
            playerStatus.EnterAimMode();
            cameraScript.SetCameraState();
            uiManager.ToggleCrosshair(true);
        }
        if (Input.GetButtonUp("Aim") || RightTrigger == TriggerState.Released)
        {
            magicManager.CastSpell();
            playerStatus.LeaveAimMode();
            cameraScript.SetCameraState();
            uiManager.ToggleCrosshair(false);
        }
    }

    void LockOnInput()
    {
        if (Input.GetButtonDown("LockOn"))
        {
            cameraPivot.ResetPosition();
            magicManager.ClearInputs();
            lockOn.LockOnPressed();
        }
        if (Input.GetButtonUp("LockOn"))
        {
            lockOn.LockOnReleased();
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
            timeSlow.Trigger();
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
