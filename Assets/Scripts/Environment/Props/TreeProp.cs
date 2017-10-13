using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeProp : ChemistryObject {

    [SerializeField]
    private GameObject fireParticleSystem;
    private GameObject fire;

    [ReadOnly]
    public Element elementState = Element.None;

    protected override void EarthInteraction()
    {
    }

    protected override void FireInteraction()
    {
        if (elementState == Element.Fire)
            return;

        elementState = Element.Fire;
        fire = Instantiate(fireParticleSystem, transform.position, transform.rotation);
    }

    protected override void TakeDamage(float damage)
    {
    }

    protected override void WaterInteraction()
    {
        if (elementState == Element.Fire)
        {
            Destroy(fire);
            elementState = Element.None;
        }
    }

    protected override void WindInteraction()
    {
    }

    protected override void IceInteraction()
    {
        print("Ice element doesn't affect trees currently");
    }
}
