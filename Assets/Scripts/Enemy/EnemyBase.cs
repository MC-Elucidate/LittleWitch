using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : ChemistryObject {

    public float Health;

	void Start () {
		
	}
	
	void Update () {
		
	}

    protected override void FireInteraction()
    {
        print("My hair is on fire!");
    }

    protected override void WaterInteraction()
    {
        print("Dance, Water! Dance!");
    }

    protected override void EarthInteraction()
    {
        print("Rock you like a hurricane!");
    }

    protected override void WindInteraction()
    {
        print("Quick as the wind!");
    }

    protected override void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject, 0f);
    }

}
