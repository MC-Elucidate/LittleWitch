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
    private Transform spellSource;
    private float lastInputTime = 0f;

    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovementScript>();
        magicManager = gameObject.GetComponent<MagicManager>();
        camera = Camera.main.GetComponent<CameraScript>();
        inputString = new StringBuilder();

        spellSource = this.gameObject.FindObjectInChildren("SpellSource").transform;
    }

    void Update()
    {
        HandleInputs();
        HandleSpellInputs();
        CheckValidSpellInputs();
    }

    private void HandleInputs()
    {
        //Movement
        playerMovement.forwardInput = Input.GetAxis("Vertical");
        playerMovement.sidewaysInput = Input.GetAxis("Horizontal");
        
        //Jump
        if (Input.GetButtonDown("Jump"))
            playerMovement.Jump();

        //Camera
        if (Input.GetButtonDown("RecentreCamera"))
            camera.RecentrePressed();
        if (Input.GetButtonUp("RecentreCamera"))
            camera.RecentreReleased();

        //Aim
        if (Input.GetButtonDown("Aim"))
        {
            ClearInputs();
            Debug.Log("Aiming!");
        }
        if (Input.GetButtonUp("Aim"))
        {
            Debug.Log("Not Aiming anymore. Cast!");
            magicManager.CastSpell(spellSource);
        }

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
            //Do we let them keep adding inputs until they get something right? Or clear it if they fill up the input list
            //inputString.Remove(0, inputString.Length - MAX_INPUTS_LENGTH);
            ClearInputs();

        Debug.Log(String.Format("Element{0} Pressed! Combo now: {1}", element, inputString));
    }

    private void CheckValidSpellInputs()
    {
        if (magicManager.CheckValidSpellInputs(inputString.ToString(), spellSource))
            ClearInputs();
    }

    private void ClearInputs()
    {
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
            ClearInputs();
            lastInputTime = 0f;
            Debug.Log("Inputs cleared! Combo is now: " + inputString);
        }
    }
}
