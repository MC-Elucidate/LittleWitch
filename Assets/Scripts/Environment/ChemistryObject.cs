using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChemistryObject : MonoBehaviour {
    

	void Start () {
		
	}
	
	void Update () {

    }

   protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Helpers.Tags.Spell)
        {
            SpellInteract(collision.gameObject.GetComponent<Spell>());
        }
    }

    protected void SpellInteract(Spell spell)
    {
        TakeDamage(spell.damage);
        ElementInteract(spell.element);
    }

    protected void ElementInteract(Element element)
    {
        switch (element)
        {
            case Element.Fire:
                FireInteraction();
                break;
            case Element.Earth:
                EarthInteraction();
                break;
            case Element.Water:
                WaterInteraction();
                break;
            case Element.Wind:
                WindInteraction();
                break;
            default:
                break;
        }
    }

    protected abstract void FireInteraction();

    protected abstract void WaterInteraction();

    protected abstract void EarthInteraction();

    protected abstract void WindInteraction();

    protected abstract void TakeDamage(float damage);
}
