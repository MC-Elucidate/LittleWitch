using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour {

	public float rotationDegreesPerSecond = 360f;

    void Start () {
		
	}
	
	void Update ()
	{
		transform.Rotate(Vector3.up * Time.deltaTime * rotationDegreesPerSecond);
	}
}
