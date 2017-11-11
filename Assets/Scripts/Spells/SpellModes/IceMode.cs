using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMode : ASpellMode {

    private GameObject coneInstance;
    private IceCone coneInstanceScript;
    private Transform spellSource;
    public GameObject IceConePrefab;

    [SerializeField]
    private float damageInterval = 0.25f;
    private float timeSinceLastDamage = 0;
    [SerializeField]
    private float costPerTick;

    private bool Casting = false;

    protected override void Start()
    {
        base.Start();
        spellSource = transform.parent.Find("SpellSource");
	}

    void Update()
    {
        if (!Casting)
            return;

        timeSinceLastDamage += Time.deltaTime;
        if (timeSinceLastDamage >= damageInterval)
        {
            timeSinceLastDamage = 0;
            if (playerStatus.UseMana(costPerTick))
                coneInstanceScript.SpellEffectTick();
            else
                EndCast();
        }
    }

    public override void AttackPressed(Vector3 spawnPosition, Vector3 spawnDirection, Vector3? targetPosition = default(Vector3?))
    {
        Casting = true;
        coneInstance = GameObject.Instantiate(IceConePrefab, spawnPosition, Quaternion.LookRotation(spellSource.forward, Vector3.up));
        coneInstanceScript = coneInstance.GetComponentInChildren<IceCone>();
        coneInstance.transform.parent = spellSource;
    }

    public override void AttackReleased(Vector3 spawnPosition, Vector3 spawnDirection, Vector3? targetPosition = default(Vector3?))
    {
        EndCast();
    }

    public override void OnSpellChangedFrom()
    {
        EndCast();
    }

    private void EndCast()
    {
        Casting = false;
        if (coneInstance != null)
        {
            Destroy(coneInstance.gameObject);
            coneInstance = null;
            coneInstanceScript = null;
        }
    }
}
