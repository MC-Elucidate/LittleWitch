using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour {

    public float TimeSlowMultipler;
    private float fixedDeltaTime;
    private PlayerMovementScript playerMovement;
    private CameraScript cameraScript;

	void Start () {
        playerMovement = gameObject.GetComponent<PlayerMovementScript>();
        cameraScript = Camera.main.GetComponent<CameraScript>();
        fixedDeltaTime = Time.fixedDeltaTime;
	}
	
	void Update () {
		
	}

    public void SlowTime()
    {
        Time.timeScale = TimeSlowMultipler;
        Time.fixedDeltaTime = fixedDeltaTime * TimeSlowMultipler;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = fixedDeltaTime;
    }

    public void SlowTimeExcludePlayer()
    {
        SlowTime();
        playerMovement.TimeSlowMovementActive(TimeSlowMultipler);
        cameraScript.TimeSlowMovementActive(TimeSlowMultipler);
    }

    public void ResumeTimeExcludePlayer()
    {
        ResumeTime();
        playerMovement.TimeSlowMovementDeactive();
        cameraScript.TimeSlowMovementDeactive();
    }
}
