using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCone : MonoBehaviour {

    private List<FreezableObject> objectsInRange;
	
    void Start ()
    {
        objectsInRange = new List<FreezableObject>();
	}
	
	public void SpellEffectTick()
    {
        foreach(FreezableObject chemObj in objectsInRange)
        {
            chemObj.TakeFrostDamage();
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
