using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMode : ASpellMode
{
    private LevitatableObject liftedObject;

    void Start () {
		
	}
	
	void Update () {
		
	}

    public override void AttackPressed(Vector3 spawnPosition, Vector3 spawnDirection)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(spawnPosition, spawnDirection, out hitInfo))
        {
            liftedObject = hitInfo.collider.GetComponent<LevitatableObject>();
            if (liftedObject == null)
                return;
            liftedObject.CastLevitate();
        }
    }

    public override void AttackReleased(Vector3 spawnPosition, Vector3 spawnDirection)
    {
    }
}
