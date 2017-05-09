﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Vector3[] points;
    public float speed;
    public int waitTime;
    private int currentIndex = 0;
    public bool isActive = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (points.Length == 0)
            return;

        if (!isActive)
            return;

        if (transform.position == points[currentIndex])
        {
            isActive = false;
            Invoke("Activate", waitTime);
            if (currentIndex == points.Length-1)
                currentIndex = 0;
            else
                currentIndex++;
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, points[currentIndex], speed);
	}

    private void Activate()
    {
        isActive = true;
    }


}
