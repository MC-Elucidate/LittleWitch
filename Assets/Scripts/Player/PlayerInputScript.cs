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
        camera = Camera.main.GetComponentInParent<CameraScript>();// gameObject.parent.parent.GetComponent<CameraScript>();
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

        //Camera
        if (Input.GetButtonDown("RecentreCamera"))
            camera.RecentrePressed();
        if (Input.GetButtonUp("RecentreCamera"))
            camera.RecentreReleased();

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
