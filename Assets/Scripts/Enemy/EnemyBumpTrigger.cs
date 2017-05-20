using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBumpTrigger : MonoBehaviour {

    void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == Helpers.Tags.Player)
        {
            collider.gameObject.GetComponent<PlayerStatus>().BumpedEnemy();
        }
    }
}
