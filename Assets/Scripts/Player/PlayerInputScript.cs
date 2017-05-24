using System;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    PlayerMovementScript playerMovement;
    MagicManager magicManager;
    UIManager uiManager;
    CameraScript cameraScript;
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
        cameraScript = Camera.main.GetComponent<CameraScript>();
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
            cameraScript.EnterAimMode();
            timeSlow.SlowTime();
            uiManager.ToggleCrosshair(true);
            Debug.Log("Aiming!");
        }
        if (Input.GetButtonUp("Aim") || RightTrigger == TriggerState.Released)
        {
            magicManager.CastSpell();
            cameraScript.LeaveAimMode();
            timeSlow.ResumeTime();
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
            timeSlow.SlowTimeExcludePlayer();
        if (Input.GetButtonUp("TimeSlow"))
            timeSlow.ResumeTimeExcludePlayer();
    }

}
