using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour {

    public float TimeSlowMultipler;
    public GameObject sphere;
    public float TimeToTriggerCameraEffect = 0.2f;
    public float TimeToDestroySphere = 1f;
    public float FocusConsumptionPerSecond = 20f;
    public float FocusRegenPerSecond = 20f;

    private float fixedDeltaTime;
    private PlayerMovementScript playerMovement;
    private PlayerStatus playerStatus;
    private CameraScript cameraScript;
    private TimeSlowTintImageEffect cameraEffect;
    private bool TimeSlowActive = false;

    void Start () {
        playerMovement = gameObject.GetComponent<PlayerMovementScript>();
        playerStatus = gameObject.GetComponent<PlayerStatus>();
        cameraScript = Camera.main.GetComponent<CameraScript>();
        cameraEffect = Camera.main.GetComponent<TimeSlowTintImageEffect>();
        fixedDeltaTime = Time.fixedDeltaTime;
	}
	
	void Update () {
        if (TimeSlowActive)
        {
            bool enoughFocus = playerStatus.UseFocus(FocusConsumptionPerSecond * Time.deltaTime / Time.timeScale);
            if (!enoughFocus)
                ResumeTimeExcludePlayer();
        }
        else
            playerStatus.RegenFocus(FocusRegenPerSecond * Time.deltaTime / Time.timeScale);
    }

    public void SlowTime()
    {
        Time.timeScale = TimeSlowMultipler;
        Time.fixedDeltaTime = fixedDeltaTime * TimeSlowMultipler;
        cameraScript.TimeSlowMovementActive(TimeSlowMultipler);
        TimeSlowActive = true;
        InstantiateSphere();
        Invoke("ToggleCameraEffectOn", TimeToTriggerCameraEffect*TimeSlowMultipler);
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = fixedDeltaTime;
        cameraScript.TimeSlowMovementDeactive();
        TimeSlowActive = false;
        ToggleCameraEffectOff();
    }

    public void SlowTimeExcludePlayer()
    {
        SlowTime();
        playerMovement.TimeSlowMovementActive(TimeSlowMultipler);
    }

    public void ResumeTimeExcludePlayer()
    {
        ResumeTime();
        playerMovement.TimeSlowMovementDeactive();
    }

    private void InstantiateSphere()
    {
        GameObject sphereInstance = Instantiate(sphere, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        sphereInstance.transform.parent = transform;
        Destroy(sphereInstance, TimeToDestroySphere*Time.timeScale);
    }

    private void ToggleCameraEffectOn()
    {
        if(TimeSlowActive)
            cameraEffect.enabled = true;
    }
    private void ToggleCameraEffectOff()
    {
        cameraEffect.enabled = false;
    }
}
