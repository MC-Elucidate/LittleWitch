using System;
using System.Collections;
using UnityEngine;

public class RotatingPlatform : Platform {

    public float duration;
    public bool counterClockwiseRotation;
    public bool counterRotatesChildren;

    public Transform[] children;
    public bool[] markedForDeactivates;

    private int RotationDirection { get { return counterClockwiseRotation ? -1 : 1; } }

    void Start ()
    {
        markedForDeactivates = new bool[children.Length];
        for (int i = 0; i < markedForDeactivates.Length; i++)
            markedForDeactivates[i] = false;
	}
	
	void Update ()
    {
        this.transform.Rotate(Vector3.up, 360 / duration * Time.smoothDeltaTime * (RotationDirection));

        if (counterRotatesChildren)
        {
            foreach (Transform child in this.transform)
                child.transform.Rotate(Vector3.up, -360 / duration * Time.smoothDeltaTime * (RotationDirection));
        }
    }

    protected override void StoodOnDropPlatform()
    {
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].gameObject.FindTaggedObjectInChildren(Helpers.Tags.Player) && !markedForDeactivates[i])
            {
                Debug.Log("I will drop!");
                markedForDeactivates[i] = true;
                StartCoroutine(Deactivate(children[i]));
                StartCoroutine(Activate(children[i], i));
            }
        }
    }

    protected override void StoodOnMovePlatform()
    {
        throw new NotSupportedException(); //Does it make sense for a rotating platform to be a moving platform too?
    }

    protected override void StoodOnSpringTrap()
    {
        foreach (Trap trap in trapComponents)
            trap.SpringTrap();
    }

    private IEnumerator Activate(Transform targetObject, int index)
    {
        yield return new WaitForSeconds(PLATFORM_DROP_RESET_TIME);
        targetObject.gameObject.SetActive(true);
        markedForDeactivates[index] = false;
    }

    private IEnumerator Deactivate(Transform targetObject)
    {
        yield return new WaitForSeconds(PLATFORM_DROP_DELAY_TIME);
        targetObject.gameObject.SetActive(false);
    }
}
