using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCone : MonoBehaviour {

    private List<ChemistryObject> objectsInRange;
    public float damageInterval = 0.25f;
    private float timeSinceLastDamage = 0;
	void Start () {
        objectsInRange = new List<ChemistryObject>();
	}
	
	void Update () {
        timeSinceLastDamage += Time.deltaTime;
        if (timeSinceLastDamage >= damageInterval)
        {
            timeSinceLastDamage = 0;
            foreach(ChemistryObject chemObj in objectsInRange)
            {
                chemObj.ChemistryInteraction(0, Element.Ice);
            }
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        ChemistryObject chemObject = collider.GetComponent<ChemistryObject>();
        if (chemObject != null)
            objectsInRange.Add(chemObject);
    }

    void OnTriggerExit(Collider collider)
    {
        ChemistryObject chemObject = collider.GetComponent<ChemistryObject>();
        if (chemObject != null)
        {
            objectsInRange.Remove(chemObject);
        }
    }
}
