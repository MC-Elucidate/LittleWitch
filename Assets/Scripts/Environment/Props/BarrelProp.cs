using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelProp : ChemistryObject {

    private float ExplosionRadius = 3;
    private float ExplosionDamage = 20;
    private bool exploded = false;
    public GameObject ExplosionEffect;

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
        print("Wind element doesn't affect barrels currently");
    }

    protected override void IceInteraction()
    {
        print("Ice element doesn't affect barrels currently");
    }

    protected override void TakeDamage(float damage)
    {
        if(damage > 0)
            Destroy(gameObject);
    }

    private void Explode()
    {
        if (exploded)
            return;
        exploded = true;
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
        GameObject explosion = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        explosion.GetComponent<Explosion>().Play();
    }

    //Shows AOE of barrel explosion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
