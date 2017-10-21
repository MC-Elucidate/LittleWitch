using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChemistryObject : MonoBehaviour {

    [ReadOnly]
    public Element elementalState = Element.None;

	void Start () {
		
	}
	
	void Update () {

    }

    protected void OnCollisionEnter(Collision collision)
    {
        ChemistryObject otherObject = collision.gameObject.GetComponent<ChemistryObject>();

        if(collision.rigidbody != null)
            TakePhysicsDamage(collision.relativeVelocity, collision.rigidbody.mass);

        if (otherObject != null)
            ChemistryInteraction(0, otherObject.elementalState);
    }

    protected void SpellInteract(Spell spell)
    {
        ChemistryInteraction(spell.damage, spell.element);
    }

    public void ChemistryInteraction(float damage, Element element)
    {
        ElementInteract(element);
        TakeDamage(damage);
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
            case Element.Ice:
                IceInteraction();
                break;
            default:
                break;
        }
    }

    private void TakePhysicsDamage(Vector3 collisionVelocity, float otherWeight)
    {
        if (collisionVelocity.sqrMagnitude > 10)
            TakeDamage(otherWeight);
    }

    protected abstract void FireInteraction();

    protected abstract void WaterInteraction();

    protected abstract void EarthInteraction();

    protected abstract void WindInteraction();

    protected abstract void IceInteraction();

    protected abstract void TakeDamage(float damage);
}
