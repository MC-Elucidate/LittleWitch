using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyAI : MonoBehaviour {

    [SerializeField]
    private float AggroDistance;
    [SerializeField]
    private float FireRate;
    [SerializeField]
    private Transform ProjectilePrefab;

    private EnemyBaseStatus status;
    private NavMeshAgent navAgent;
    private Transform player;
    private Transform spellSource;
    private float lastShotTime;

	void Start () {
        status = gameObject.GetComponent<EnemyBaseStatus>();
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(Helpers.Tags.Player).transform;
        spellSource = this.gameObject.FindObjectInChildren("SpellSource").transform;
        navAgent.Stop();
	}
	
	void Update () {
        if (status.IsDead())
        {
            Die();
        }
        else if (status.IsFrozen())
            return;
        else
        {
            if (lastShotTime > 0)
                lastShotTime -= Time.deltaTime;

            float distanceToPlayer = (transform.position - player.position).sqrMagnitude;
            if (distanceToPlayer < AggroDistance)
                Aggro();
            else
                DeAggro();

            if (lastShotTime <= 0 && status.IsAggro())
                Shoot();

            if (status.IsAggro())
            {
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            }
        }
	}

    void Aggro()
    {
        if (status.IsDead() || status.IsAggro())
            return;

        status.AggroOnPlayer();
    }

    void DeAggro()
    {
        if (status.IsDead() || status.IsIdle())
            return;

        status.BecomeIdle();
    }

    void Die()
    {
        navAgent.Stop();
    }

    private void Shoot()
    {
        lastShotTime = FireRate;
        var playerCentrePosition = (player.position + new Vector3(0, player.GetComponent<CharacterController>().height / 2, 0));
        GameObject.Instantiate(ProjectilePrefab, spellSource.position, Quaternion.LookRotation(playerCentrePosition - spellSource.position));
    }
}
