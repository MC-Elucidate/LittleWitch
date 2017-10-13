using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    PlayerMovementManager playerMovement;
    MagicManager magicManager;
    UIManager uiManager;
    PlayerStatus playerStatus;
    CameraManager cameraManager;
    CameraPositionPivotManager cameraPivot;
    LockOnManager lockOn;

    [SerializeField]
    private float mouseSensitivity = 50f;
    [SerializeField]
    private float controllerSensitivity = 180f;

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
    AxisState DownDPad;
    AxisState UpDPad;

    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovementManager>();
        magicManager = gameObject.GetComponent<MagicManager>();
        uiManager = gameObject.GetComponent<UIManager>();
        cameraManager = Camera.main.GetComponent<CameraManager>();
        cameraPivot = gameObject.GetComponentInChildren<CameraPositionPivotManager>();
        playerStatus = gameObject.GetComponent<PlayerStatus>();
        lockOn = gameObject.GetComponent<LockOnManager>();
        RightTrigger = AxisState.NotHeld;
        RightDPad = AxisState.NotHeld;
        LeftDPad = AxisState.NotHeld;
        UpDPad = AxisState.NotHeld;
        DownDPad = AxisState.NotHeld;
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

        if (Input.GetAxisRaw("SelectSpell2") == 1 && UpDPad == AxisState.NotHeld)
        {
            UpDPad = AxisState.Pressed;
        }
        else if (Input.GetAxisRaw("SelectSpell2") <= 0 && UpDPad == AxisState.Held)
        {
            UpDPad = AxisState.Released;
        }

        if (Input.GetAxisRaw("SelectSpell2") == -1 && DownDPad == AxisState.NotHeld)
        {
            DownDPad = AxisState.Pressed;
        }
        else if (Input.GetAxisRaw("SelectSpell2") >= 0 && DownDPad == AxisState.Held)
        {
            DownDPad = AxisState.Released;
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

        if (UpDPad == AxisState.Pressed)
            UpDPad = AxisState.Held;
        else if (UpDPad == AxisState.Released)
            UpDPad = AxisState.NotHeld;

        if (DownDPad == AxisState.Pressed)
            DownDPad = AxisState.Held;
        else if (DownDPad == AxisState.Released)
            DownDPad = AxisState.NotHeld;
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
        float controllerInputVertical = Input.GetAxis("CameraVerticalC") * controllerSensitivity;
        float controllerInputHorizontal = Input.GetAxis("CameraHorizontalC") * controllerSensitivity;
        if (controllerInputHorizontal == 0)
        {
            cameraManager.yawInput = Input.GetAxis("CameraHorizontalM") * mouseSensitivity;
            playerMovement.yawInput = Input.GetAxis("CameraHorizontalM") * mouseSensitivity;
        }
        else
        {
            cameraManager.yawInput = controllerInputHorizontal;
            playerMovement.yawInput = controllerInputHorizontal;
        }

        if (controllerInputVertical == 0)
        {
            cameraManager.pitchInput = Input.GetAxis("CameraVerticalM") * mouseSensitivity;
            cameraPivot.pitchInput = Input.GetAxis("CameraVerticalM") * mouseSensitivity;
        }
        else
        {
            cameraManager.pitchInput = controllerInputVertical;
            cameraPivot.pitchInput = controllerInputVertical;
        }
    }

    void AimInput()
    {
        if (Input.GetButtonDown("Aim") || RightTrigger == AxisState.Pressed)
        {
            cameraPivot.ResetPosition();
            playerStatus.EnterAimMode();
            cameraManager.SetCameraState();
            uiManager.ToggleCrosshair(true);
        }
        if (Input.GetButtonUp("Aim") || RightTrigger == AxisState.Released)
        {
            playerStatus.LeaveAimMode();
            cameraManager.SetCameraState();
            uiManager.ToggleCrosshair(false);
        }
    }

    void LockOnInput()
    {
        if (Input.GetButtonDown("LockOn"))
        {
            cameraPivot.ResetPosition();
            lockOn.HardLockOnPressed();
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
        if (UpDPad == AxisState.Pressed)
        {
            magicManager.ActivateIceMode();
        }
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
