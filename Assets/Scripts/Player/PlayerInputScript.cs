using System;
using System.Text;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    PlayerMovementScript playerMovement;
    MagicManager magicManager;
    CameraScript camera;

    private StringBuilder inputString;
    private const int MAX_INPUTS_LENGTH = 8;
    private const float INPUTS_CLEAR_TIME = 2.5f;

    private float lastInputTime = 0f;

    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovementScript>();
        magicManager = gameObject.GetComponent<MagicManager>();
        camera = Camera.main.GetComponent<CameraScript>();
        inputString = new StringBuilder();
    }

    void Update()
    {
        HandleInputs();
        ManageInputString();

        if (magicManager.Cast(inputString.ToString(), transform.position + transform.forward * 1.5f))
            inputString.Remove(0, inputString.Length);
    }

    private void HandleInputs()
    {
        playerMovement.forwardInput = Input.GetAxis("Vertical");
        playerMovement.sidewaysInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
            playerMovement.Jump();

        for (int i = 1; i <= 3; i++)
        {
            if (Input.GetButtonDown("Element" + i))
                AddToInputString(i);
        }
    }

    private void ManageInputString()
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

    private void AddToInputString(int element)
    {
        lastInputTime = 0f;
        inputString.Append(element);

        if (inputString.Length >= MAX_INPUTS_LENGTH)
            inputString.Remove(0, inputString.Length - MAX_INPUTS_LENGTH);

        Debug.Log(String.Format("Element{0} Pressed! Combo now: {1}", element, inputString));
    }
}
