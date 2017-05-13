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
        collider.gameObject.SendMessage("OutOfBounds");
    }
}
