using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ChemistryObject))]
public class LevitatableObject : MonoBehaviour {

    private bool levitated = false;
    private bool reachedHeight = false;
    private Vector3 destination;
    private Vector3 startPosition;
    private Rigidbody rigidbody;
    private ChemistryObject chemistryObject;
    
    public float LevitationHeight = 10;
    public float TimeToRise = 3f;
    public float MaxFloatTime = 10;
    private float currentFloatTime;

	public void Start () {
        rigidbody = GetComponent<Rigidbody>();
        chemistryObject = GetComponent<ChemistryObject>();
	}

	public void Update () {
        if (levitated)
        {
            currentFloatTime += Time.deltaTime;
            if (!reachedHeight)
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

        levitated = true;
        rigidbody.useGravity = false;
        startPosition = transform.position;
        destination = new Vector3(transform.position.x, transform.position.y + LevitationHeight, transform.position.z);
        chemistryObject.ChemistryInteraction(0, Element.Wind);
    }

    public void EndLevitate()
    {
        levitated = false;
        reachedHeight = false;
        rigidbody.useGravity = true;
        currentFloatTime = 0f;
    }

    public void CastLevitate()
    {
        if (levitated)
            EndLevitate();
        else
            Levitate();
    }
}
