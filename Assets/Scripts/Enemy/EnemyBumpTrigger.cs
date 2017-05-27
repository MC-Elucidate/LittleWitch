using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBumpTrigger : MonoBehaviour {

    private BumpEnemyStatus status;

    void Start () {
        status = gameObject.GetComponent<BumpEnemyStatus>();
	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (status.IsDead())
            return;
        if (collider.gameObject.tag == Helpers.Tags.Player)
        {
            collider.gameObject.GetComponent<PlayerDamageManager>().BumpedEnemy();
        }
    }
}
