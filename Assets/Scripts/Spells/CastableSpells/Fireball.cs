using System;
using UnityEngine;

[Serializable]
public class Fireball : Spell
{
    public Transform explosionPrefab;
    private float totalDistanceTravelled = 0f;
    
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

    private void MoveProjectile()
    {
        this.transform.DebugDirectionRay();
        var distanceToTravel = transform.forward * speed * Time.deltaTime;
        totalDistanceTravelled += distanceToTravel.magnitude;
        this.rigidBody.MovePosition(this.transform.position += distanceToTravel);

        if (totalDistanceTravelled >= range)
            Destroy(this.gameObject);

    }
}
