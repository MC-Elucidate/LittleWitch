using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ChemistryObject))]
public class LevitatableObject : MonoBehaviour {

    public bool levitated = false;
    private bool reachedHeight = false;
    private Vector3 destination;
    private Vector3 startPosition;
    private Transform grabbedLocation;
    private Rigidbody rigidbody;
    private ChemistryObject chemistryObject;

    public float ThrowPower = 25f;
    
    public float LevitationHeight = 10;
    public float TimeToRise = 3f;
    public float MaxFloatTime = 10;
    private float currentFloatTime;

    public bool grabbed = false;

	public void Start () {
        rigidbody = GetComponent<Rigidbody>();
        chemistryObject = GetComponent<ChemistryObject>();
	}

	public void Update () {
        if (levitated)
        {
            currentFloatTime += Time.deltaTime;

            if (grabbed)
            {
                if((transform.position - grabbedLocation.position).sqrMagnitude > .25f)
                    transform.position = Vector3.Lerp(transform.position, grabbedLocation.position, 0.1f);
            }

            else if (!reachedHeight)
            {
                transform.position = Vector3.Lerp(startPosition, destination, currentFloatTime / TimeToRise);

                if (currentFloatTime >= TimeToRise)
                {
                    reachedHeight = true;
                    currentFloatTime = 0f;
                }
            }

            else
            {
                if (currentFloatTime >= MaxFloatTime)
                    EndLevitate();
            }
        }
	}

    public void Levitate()
    {
        if (levitated)
            return;

        chemistryObject.ChemistryInteraction(0, Element.Wind);

        levitated = true;
        rigidbody.useGravity = false;
        startPosition = transform.position;

        destination = new Vector3(transform.position.x, transform.position.y + LevitationHeight, transform.position.z);
    }

    public void EndLevitate()
    {
        levitated = false;
        reachedHeight = false;
        rigidbody.useGravity = true;
        currentFloatTime = 0f;
    }

    public void Grab(Transform grabbedLocation)
    {
        Levitate();
        grabbed = true;
        this.grabbedLocation = grabbedLocation;
    }

    public void ThrowAtTarget(Vector3 targetLocation)
    {
        ThrowInDirection(targetLocation - transform.position);
    }

    public void ThrowInDirection(Vector3 throwDirection)
    {
        EndLevitate();
        grabbed = false;
        rigidbody.velocity = throwDirection.normalized * ThrowPower;
    }
}
