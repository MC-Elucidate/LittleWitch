using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour {

	public float rotationDegreesPerSecond = 360f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(Vector3.up * Time.deltaTime * rotationDegreesPerSecond);
	}
}
