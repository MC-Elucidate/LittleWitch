using UnityEngine;

public class BumpEnemyTrigger : MonoBehaviour {

    private EnemyBaseStatus status;

    void Start ()
    {
        status = gameObject.GetComponent<EnemyBaseStatus>();
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
