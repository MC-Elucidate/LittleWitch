using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour {

    // Use this for initialization
    public float rotationSpeed = 0;

	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        foreach (Transform child in this.transform)
            child.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }
}
