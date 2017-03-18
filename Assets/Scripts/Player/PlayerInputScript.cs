using System;
using System.Text;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    PlayerMovementScript playerMovement;
    MagicManager magicManager;
    CameraScript camera;

    private const int MAX_INPUTS_LENGTH = 8;
    private const float INPUTS_CLEAR_TIME = 2.5f;

    private StringBuilder inputString;
    private Transform leftHand;
    private Transform rightHand;
    private float lastInputTime = 0f;

    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovementScript>();
        magicManager = gameObject.GetComponent<MagicManager>();
        camera = Camera.main.GetComponent<CameraScript>();
        inputString = new StringBuilder();

        leftHand = this.gameObject.FindObjectInChildren("LeftHand").transform;
        rightHand = this.gameObject.FindObjectInChildren("RightHand").transform;
    }

    void Update()
    {
        HandleInputs();
        HandleSpellInputs();
        CheckValidSpellInputs();
    }

    private void HandleInputs()
    {
        playerMovement.forwardInput = Input.GetAxis("Vertical");
        playerMovement.sidewaysInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
            playerMovement.Jump();

        if (Input.GetButtonDown("RecentreCamera"))
            camera.RecentrePressed();
        if (Input.GetButtonUp("RecentreCamera"))
            camera.RecentreReleased();

        for (int i = 1; i <= 3; i++)
        {
            if (Input.GetButtonDown("Element" + i))
                AddToInputString(i);
        }
    }

    private void AddToInputString(int element)
    {
        lastInputTime = 0f;
        inputString.Append(element);

        if (inputString.Length >= MAX_INPUTS_LENGTH)
            inputString.Remove(0, inputString.Length - MAX_INPUTS_LENGTH);

        Debug.Log(String.Format("Element{0} Pressed! Combo now: {1}", element, inputString));
    }

    private void CheckValidSpellInputs()
    {
        if (magicManager.CheckValidSpellInputs(inputString.ToString(), rightHand))
            inputString.Remove(0, inputString.Length);
    }

    private void HandleSpellInputs()
    {
        if (inputString.Length > 0)
            lastInputTime += Time.deltaTime;
        else
            lastInputTime = 0f;

        if (lastInputTime >= INPUTS_CLEAR_TIME)
        {
            inputString.Remove(0, inputString.Length);
            lastInputTime = 0f;
            Debug.Log("Inputs cleared! Combo is now: " + inputString);
        }
    }
}
