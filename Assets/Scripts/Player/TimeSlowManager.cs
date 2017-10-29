using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TimeSlowManager : MonoBehaviour {

    public float TimeSlowMultipler;
    public GameObject TimeSlowSphere;
    public float FocusConsumptionPerSecond = 20f;
    public float FocusRegenPerSecond = 20f;
    public AudioMixerSnapshot NormalSnapshot;
    public AudioMixerSnapshot TimeSlowedSnapshot;
    public GameObject AudioSourceGameObject;
    public AudioClip StartClip;
    public AudioClip EndClip;

    private float fixedDeltaTime;
    private float timeToTriggerCameraEffect = 0.2f;
    private float timeToDestroySphere = 1f;
    private PlayerMovementManager playerMovement;
    private PlayerStatus playerStatus;
    private CameraManager cameraScript;
    private TimeSlowTintImageEffect cameraEffect;
    private bool TimeSlowActive = false;
    private AudioSource loopSource;
    private AudioSource startEndSource;

    void Start () {
        playerMovement = gameObject.GetComponent<PlayerMovementManager>();
        playerStatus = gameObject.GetComponent<PlayerStatus>();
        cameraScript = Camera.main.GetComponent<CameraManager>();
        cameraEffect = Camera.main.GetComponent<TimeSlowTintImageEffect>();
        fixedDeltaTime = Time.fixedDeltaTime;

        AudioSource[] audioSources = AudioSourceGameObject.GetComponents<AudioSource>();
        startEndSource = audioSources[0];
        loopSource = audioSources[1];
	}
	
	void Update () {
        if (TimeSlowActive)
        {
            bool enoughFocus = playerStatus.UseMana(FocusConsumptionPerSecond * Time.deltaTime / Time.timeScale);
            if (!enoughFocus)
                ResumeTimeExcludePlayer();
        }
        else
            playerStatus.RegenFocus(FocusRegenPerSecond * Time.deltaTime / Time.timeScale);
    }

    public void Trigger()
    {
        if (TimeSlowActive)
            ResumeTimeExcludePlayer();
        else
            SlowTimeExcludePlayer();
    }

    public void SlowTime()
    {
        Time.timeScale = TimeSlowMultipler;
        Time.fixedDeltaTime = fixedDeltaTime * TimeSlowMultipler;
        cameraScript.TimeSlowMovementActive(TimeSlowMultipler);
        TimeSlowActive = true;
        InstantiateSphere();
        TimeSlowedSnapshot.TransitionTo(0f);
        startEndSource.PlayOneShot(StartClip);
        loopSource.Play();
        Invoke("ToggleCameraEffectOn", timeToTriggerCameraEffect*TimeSlowMultipler);
    }

    public void ResumeTime()
    {
        if (!TimeSlowActive)
            return;
        Time.timeScale = 1;
        Time.fixedDeltaTime = fixedDeltaTime;
        cameraScript.TimeSlowMovementDeactive();
        TimeSlowActive = false;
        NormalSnapshot.TransitionTo(0f);
        startEndSource.PlayOneShot(EndClip);
        loopSource.Stop();
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
        GameObject sphereInstance = Instantiate(TimeSlowSphere, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        sphereInstance.transform.parent = transform;
        Destroy(sphereInstance, timeToDestroySphere*Time.timeScale);
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
