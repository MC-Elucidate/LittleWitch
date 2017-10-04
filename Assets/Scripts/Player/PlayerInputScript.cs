﻿using System;
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

    private enum AxisState
    {
        Pressed = 1,
        Released = 2,
        Held = 3,
        NotHeld = 4
    }

    AxisState RightTrigger;
    AxisState LeftDPad;
    AxisState RightDPad;

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
        RightTrigger = AxisState.NotHeld;
        RightDPad = AxisState.NotHeld;
        LeftDPad = AxisState.NotHeld;
        HideCursor();
    }

    void Update()
    {
        CheckAxes();
        HandleInputs();
        SaveAxesStates();
    }

    private void HandleInputs()
    {
        MovementInput();
        CameraInput();
        AimInput();
        LockOnInput();
        SpellInput();
        SpellModeInput();
    }

    #region InputForAxes
    private void CheckAxes()
    {
        if (Input.GetAxisRaw("AimTrigger") == 1 && RightTrigger == AxisState.NotHeld)
        {
            RightTrigger = AxisState.Pressed;
        }
        else if (Input.GetAxisRaw("AimTrigger") == 0 && RightTrigger == AxisState.Held)
        {
            RightTrigger = AxisState.Released;
        }

        if (Input.GetAxisRaw("SelectSpell1") == 1 && RightDPad == AxisState.NotHeld)
        {
            RightDPad = AxisState.Pressed;
        }
        else if (Input.GetAxisRaw("SelectSpell1") <= 0 && RightDPad == AxisState.Held)
        {
            RightDPad = AxisState.Released;
        }

        if (Input.GetAxisRaw("SelectSpell1") == -1 && LeftDPad == AxisState.NotHeld)
        {
            LeftDPad = AxisState.Pressed;
        }
        else if (Input.GetAxisRaw("SelectSpell1") >= 0 && LeftDPad == AxisState.Held)
        {
            LeftDPad = AxisState.Released;
        }
    }

    private void SaveAxesStates()
    {
        if (RightTrigger == AxisState.Pressed)
            RightTrigger = AxisState.Held;
        else if (RightTrigger == AxisState.Released)
            RightTrigger = AxisState.NotHeld;

        if (RightDPad == AxisState.Pressed)
            RightDPad = AxisState.Held;
        else if (RightDPad == AxisState.Released)
            RightDPad = AxisState.NotHeld;

        if (LeftDPad == AxisState.Pressed)
            LeftDPad = AxisState.Held;
        else if (LeftDPad == AxisState.Released)
            LeftDPad = AxisState.NotHeld;
    }
    #endregion

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
        if (Input.GetButtonDown("Aim") || RightTrigger == AxisState.Pressed)
        {
            cameraPivot.ResetPosition();
            playerStatus.EnterAimMode();
            cameraScript.SetCameraState();
            uiManager.ToggleCrosshair(true);
        }
        if (Input.GetButtonUp("Aim") || RightTrigger == AxisState.Released)
        {
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
            lockOn.LockOnPressed();
        }
        if (Input.GetButtonUp("LockOn"))
        {
            lockOn.LockOnReleased();
        }
    }

    void SpellInput()
    {
         if (Input.GetButtonDown("BasicAttack"))
            magicManager.BasicAttackPressed();
        else if (Input.GetButtonUp("BasicAttack"))
            magicManager.BasicAttackReleased();
    }

    void SpellModeInput()
    {
        if (RightDPad == AxisState.Pressed)
        {
            magicManager.ActivateWindMode();
        }
        if (LeftDPad == AxisState.Pressed)
        {
            magicManager.ActivateFireMode();
        }
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
