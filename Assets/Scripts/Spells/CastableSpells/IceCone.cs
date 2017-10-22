using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCone : MonoBehaviour {

    private List<FreezableObject> objectsInRange;

    public float damageInterval = 0.25f;
    private float timeSinceLastDamage = 0;
	void Start () {
        objectsInRange = new List<FreezableObject>();
	}
	
	void Update () {
        timeSinceLastDamage += Time.deltaTime;
        if (timeSinceLastDamage >= damageInterval)
        {
            timeSinceLastDamage = 0;
            foreach(FreezableObject chemObj in objectsInRange)
            {
                chemObj.TakeFrostDamage();
            }
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        FreezableObject freezableObject = collider.GetComponent<FreezableObject>();
        if (freezableObject != null)
            objectsInRange.Add(freezableObject);
    }

    void OnTriggerExit(Collider collider)
    {
        FreezableObject freezableObject = collider.GetComponent<FreezableObject>();
        if (freezableObject != null)
        {
            objectsInRange.Remove(freezableObject);
        }
    }
}
