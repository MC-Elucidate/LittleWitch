using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMode : ASpellMode
{
	public SpellChargeParticleModule spellChargeParticleModule;

	private LevitatableObject liftedObject;
    private bool objectGrabbed = false;
    private bool objectLifted = false;
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
                    
                if (objectLifted && newLiftedObject == liftedObject)
                {
                    liftedObject.EndLevitate();
                    objectLifted = false;
                }
                else
                {
                    if (objectLifted)
                        liftedObject.EndLevitate();
                    objectLifted = true;
                    liftedObject = newLiftedObject;
                    liftedObject.Levitate();
                }
            }
            
        }
    }

    public override void AttackReleased(Vector3 spawnPosition, Vector3 spawnDirection, Vector3? targetPosition = null)
    {
        buttonHeld = false;
        timeButtonHeld = 0;

        if (!objectLifted && !objectGrabbed)
            liftedObject = null;

        if (liftedObject)
            spellChargeParticleModule.ClearParticleEffects();
        else
            spellChargeParticleModule.ClearAllEffects();
    }

	private void GrabObject()
    {
        if (objectGrabbed)
            return;
        if (liftedObject == null)
            return;
        
        objectGrabbed = true;
        objectLifted = false;
        liftedObject.Grab(grabbedLocation);
		spellChargeParticleModule.ClearParticleEffects();
	}

    public override void OnSpellChangedFrom()
    {

        if (liftedObject != null)
        {
            if (objectGrabbed)
                liftedObject.ThrowAtTarget(liftedObject.transform.position);

            else if (objectLifted)
                liftedObject.EndLevitate();
        }

       
        objectGrabbed = false;
        objectLifted = false;
        buttonHeld = false;
        timeButtonHeld = 0;
        liftedObject = null;
            

        spellChargeParticleModule.ClearAllEffects();

    }
}
