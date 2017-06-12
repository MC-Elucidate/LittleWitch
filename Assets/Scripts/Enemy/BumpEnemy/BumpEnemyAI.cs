using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BumpEnemyAI : MonoBehaviour {

    private EnemyBaseStatus status;
    private NavMeshAgent navAgent;
    private Transform player;
    public float AggroDistance;

	void Start () {
        status = gameObject.GetComponent<EnemyBaseStatus>();
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(Helpers.Tags.Player).transform;
        navAgent.Stop();
	}
	
	void Update () {
        if (status.IsDead())
        {
            Die();
        }
        else
        {
            float distanceToPlayer = (new Vector2(transform.position.x, transform.position.z) - new Vector2(player.position.x, player.position.z)).sqrMagnitude;
            if (distanceToPlayer < AggroDistance)
                Aggro();
            else
                DeAggro();
        }
	}

    void Aggro()
    {
        navAgent.SetDestination(player.position);
        if (status.IsDead() || status.IsAggro())
            return;

        status.AggroOnPlayer();
        navAgent.Resume();
    }

    void DeAggro()
    {
        if (status.IsDead() || status.IsIdle())
            return;

        status.BecomeIdle();
        navAgent.Stop();
    }

    void Die()
    {
        navAgent.Stop();
    }
}
