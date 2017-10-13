using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {
    
	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == Helpers.Tags.Player)
        {
            collider.gameObject.GetComponent<PlayerStatus>().OutOfBounds();
        }
    }
}
