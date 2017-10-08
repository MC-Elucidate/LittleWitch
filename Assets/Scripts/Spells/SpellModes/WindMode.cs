using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMode : ASpellMode
{
	public SpellChargeParticleModule spellChargeParticleModule;

	private LevitatableObject liftedObject;
    private bool objectGrabbed = false;
    private bool buttonHeld = false;
    private float timeButtonHeld = 0f;
    private Transform grabbedLocation;

    public float TimeToGrab = 1;

    void Start () {
        grabbedLocation = Helpers.FindObjectInChildren(transform.parent.gameObject, "GrabbedLocation").transform;
	}
	
	void Update () {
        if (buttonHeld)
        {
            timeButtonHeld += Time.deltaTime;
			spellChargeParticleModule.EmitChargingParticles(timeButtonHeld, TimeToGrab);

			if (timeButtonHeld > TimeToGrab)
                GrabObject();
        }
	}

    public override void AttackPressed(Vector3 spawnPosition, Vector3 spawnDirection, Vector3? targetPosition = null)
    {
        buttonHeld = true;

        if (objectGrabbed)
        {
            if(targetPosition.HasValue)
                liftedObject.ThrowAtTarget(targetPosition.Value);
            else
                liftedObject.ThrowInDirection(spawnDirection);
            liftedObject = null;
            objectGrabbed = false;
        }

        else
        {
            
            RaycastHit hitInfo;
            if (Physics.Raycast(spawnPosition, spawnDirection, out hitInfo))
            {
                LevitatableObject newLiftedObject = hitInfo.collider.GetComponent<LevitatableObject>();
                if (newLiftedObject == null)
                    return;

                
                if (liftedObject != null && newLiftedObject.name == liftedObject.name)
                {
                    liftedObject = null;
                }
                else
                {
                    liftedObject = newLiftedObject;
                }
                liftedObject.CastLevitate();
			}
        }
    }

    public override void AttackReleased(Vector3 spawnPosition, Vector3 spawnDirection, Vector3? targetPosition = null)
    {
        buttonHeld = false;
        timeButtonHeld = 0;

		if (liftedObject)
			spellChargeParticleModule.ClearParticleEffects();
		else
			spellChargeParticleModule.ClearAllEffects();
	}

	private void GrabObject()
    {
        if (liftedObject == null)
            return;

        objectGrabbed = true;
        liftedObject.Grab(grabbedLocation);
		spellChargeParticleModule.ClearParticleEffects();
	}
}
