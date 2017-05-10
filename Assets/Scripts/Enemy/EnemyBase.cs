using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {

    public float Health;

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Helpers.Tags.Spell)
        {
            SpellInteract(collision.gameObject.GetComponent<Spell>());
        }
    }

    void SpellInteract(Spell spell)
    {
        TakeDamage(spell.damage);
        ElementInteract(spell.element);
    }

    void ElementInteract(Element element)
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

    void FireInteraction()
    {
        print("My hair is on fire!");
    }

    void WaterInteraction()
    {
        print("Soaking wet!");
    }

    void EarthInteraction()
    {
        print("Rock 'n' more rock!");
    }

    void WindInteraction()
    {
        print("Quick as the wind!");
    }

    void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Destroy(gameObject, 1.5f);
    }

}
