using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMode : ASpellMode {

    private bool held = false;
    private GameObject coneInstance;
    private Transform spellSource;
    private IceCone iceCone;
    public GameObject IceConePrefab;

    void Start () {
        spellSource = transform.parent.FindChild("SpellSource");
	}
	
	void Update () {
		
	}

    public override void AttackPressed(Vector3 spawnPosition, Vector3 spawnDirection, Vector3? targetPosition = default(Vector3?))
    {
        coneInstance = GameObject.Instantiate(IceConePrefab, spawnPosition, Quaternion.LookRotation(spellSource.forward, Vector3.up));
        iceCone = coneInstance.GetComponentInChildren<IceCone>();
        coneInstance.transform.parent = spellSource;
        held = true;
    }

    public override void AttackReleased(Vector3 spawnPosition, Vector3 spawnDirection, Vector3? targetPosition = default(Vector3?))
    {
        Destroy(coneInstance);
        held = false;
    }
}
