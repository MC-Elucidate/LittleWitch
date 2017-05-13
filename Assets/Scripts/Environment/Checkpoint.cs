using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == Helpers.Tags.Player)
        {
            PlayerStatus player = collider.gameObject.GetComponent<PlayerStatus>();
            player.CheckpointTouched(transform);
        }
    }
}
