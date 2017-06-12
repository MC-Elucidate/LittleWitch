using System;
using System.Linq;
using UnityEngine;

public class MovingPlatform : Platform {

    public Vector3[] points;
    public float duration;
    public int waitTime;
    public bool isMoving = true;

    private int currentIndex = 0;
    private float currentDuration = 0;

	void Start ()
    {

	}
	
	void Update ()
    {
        if (points.Length == 0)
            return;

        if (!isMoving)
            return;
        else
            currentDuration += Time.smoothDeltaTime;

        if (transform.position == points[currentIndex])
        {
            isMoving = false;
            if (platformType != PlatformType.Move)
                Invoke("Move", waitTime);

            currentDuration = 0;
            if (currentIndex == points.Length - 1)
                currentIndex = 0;
            else
                currentIndex++;
        }
        else
            transform.position = Vector3.Lerp(GetPreviousIndex(), points[currentIndex], Mathf.SmoothStep(0f, 1f, currentDuration / duration));
    }

    protected override void StoodOnSpringTrap()
    {
        foreach (Trap trap in trapComponents)
            trap.SpringTrap();
    }

    protected override void StoodOnDropPlatform()
    {
        //Play Animation
        if (!markedForDeactivate)
        { 
            Debug.Log("I will drop!");
            markedForDeactivate = true;
            Invoke("Deactivate", PLATFORM_DROP_DELAY_TIME);
            Invoke("Activate", PLATFORM_DROP_RESET_TIME);
        }
       
    }

    protected override void StoodOnMovePlatform()
    {
        if (!isMoving)
        {
            Debug.Log("Triggered the Move");
            Invoke("Move", PLATFORM_MOVE_DELAY_TIME);
        }
    }

    private void Move()
    {
        isMoving = true;
    }

    private void Activate()
    {
        this.gameObject.SetActive(true);
        markedForDeactivate = false;
    }

    private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    private Vector3 GetPreviousIndex()
    {
        if ((currentIndex - 1) < 0)
            return points[points.Length - 1];
        else
            return points[currentIndex - 1];
    }
}
