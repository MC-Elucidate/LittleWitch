using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairPositioner : MonoBehaviour {

    public Sprite[] spellCrosshairs;

    private Camera mainCamera;
    private const float scale = 0.5f;
    private const float maxDistance = 30f; //This can change depending on which spell we have
	// Use this for initialization
	void Start ()
    {
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateCrosshair();
	}

    void UpdateCrosshair()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, maxDistance))
        {
            transform.position = hit.point + (0.2f * hit.normal);
            transform.forward = hit.normal;
            //transform.localScale = new Vector3(scale, scale, scale);
        }
        else
        {
            transform.position = mainCamera.transform.position + mainCamera.transform.forward * maxDistance;
            transform.forward = -mainCamera.transform.forward;
            //transform.localScale = Vector3.zero;
        }
    }
}
