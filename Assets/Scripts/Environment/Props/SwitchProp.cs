﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchProp : ChemistryObject {

    [ReadOnly]
    [SerializeField]
    private bool active = false;
    private TriggerableObject[] objectsToTrigger;

    [SerializeField]
    private GameObject[] connectedObjects;

    #region ElementInteractions
    protected override void EarthInteraction()
    {
    }

    protected override void FireInteraction()
    {
    }

    protected override void WaterInteraction()
    {
    }

    protected override void WindInteraction()
    {
    }

    protected override void IceInteraction()
    {
    }
    #endregion

    protected override void TakeDamage(float damage)
    {
        active = !active;
        foreach (TriggerableObject objectToTrigger in objectsToTrigger)
        {
            objectToTrigger.Trigger(active);
        }
    }

    void Start () {
        objectsToTrigger = new TriggerableObject[connectedObjects.Length];
        for (int i = 0; i < connectedObjects.Length; i++)
        {
            objectsToTrigger[i] = connectedObjects[i].GetComponent<TriggerableObject>();
        }
	}
}
