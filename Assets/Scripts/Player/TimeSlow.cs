using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour {

    public float TimeSlowMultipler;
    private float fixedDeltaTime;

	void Start () {
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
}
