using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour {

    protected float destroyTime;
    private bool collected = false;

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collected)
            return;
        if (collider.gameObject.tag == Helpers.Tags.Player)
        {
            collected = true;
            PickupEffect(collider.gameObject);
            Destroy(gameObject, destroyTime);
        }
    }

    public abstract void PickupEffect(GameObject player);
}
