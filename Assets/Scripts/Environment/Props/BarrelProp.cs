using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelProp : ChemistryObject {

    private float ExplosionRadius = 3;
    private float ExplosionDamage = 20;

    protected override void FireInteraction()
    {
        Explode();
    }

    protected override void WaterInteraction()
    {
        throw new NotImplementedException();
    }

    protected override void EarthInteraction()
    {
        throw new NotImplementedException();
    }

    protected override void WindInteraction()
    {
        throw new NotImplementedException();
    }

    protected override void TakeDamage(float damage)
    {
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (Collider objectHit in objectsInRange)
        {
            if (objectHit.isTrigger) //Ignores non-physical objects
                continue;

            if (objectHit.name == gameObject.name) //Ignore self
                continue;
            
            ChemistryObject chemistry = objectHit.GetComponent<ChemistryObject>();
            if (chemistry != null)
            {
                chemistry.ChemistryInteraction(ExplosionDamage, Element.Fire);
            }
        }
    }

    //Shows AOE of barrel explosion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }

}
