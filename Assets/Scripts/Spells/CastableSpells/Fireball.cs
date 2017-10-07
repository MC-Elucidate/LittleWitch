using System;
using UnityEngine;

[Serializable]
public class Fireball : Spell
{
    public Transform explosionPrefab;

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        this.MoveProjectile();
    }

    public override void Trigger()
    {
        Debug.Log("Destroyed " + this.name);
        //Instantiate(explosionPrefab);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        ChemistryObject chemObj = collider.GetComponent<ChemistryObject>();
        if (chemObj != null)
        {
            chemObj.ChemistryInteraction(damage, element);
            Trigger();
        }
    }
}
