using System;
using UnityEngine;

public class RotatingPlatform : Platform {

    public float duration;

    void Start ()
    {

	}
	
	void Update ()
    {
        this.transform.Rotate(Vector3.up, 360 / duration * Time.smoothDeltaTime);

        //Rotate children around themselves
        foreach (Transform child in this.transform)
            child.transform.Rotate(Vector3.up, -360 / duration * Time.smoothDeltaTime);
    }

    protected override void StoodOnDropPlatform()
    {
        throw new NotImplementedException();
    }

    protected override void StoodOnMovePlatform()
    {
        throw new NotImplementedException();
    }

    protected override void StoodOnSpringTrap()
    {
        throw new NotImplementedException();
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
}
