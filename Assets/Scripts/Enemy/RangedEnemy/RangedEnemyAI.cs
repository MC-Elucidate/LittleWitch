using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyAI : MonoBehaviour {

    public float AggroDistance;
    public Transform ProjectilePrefab;
    public float FireRate;

    private RangedEnemyStatus status;
    private EnemySoundManager sounds;
    private NavMeshAgent navAgent;
    private Transform player;
    private Transform spellSource;
    private float lastShotTime;

	void Start () {
        status = gameObject.GetComponent<RangedEnemyStatus>();
        sounds = gameObject.GetComponent<EnemySoundManager>();
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
        }
	}

    void Aggro()
    {
        //navAgent.SetDestination(player.position);
        if (status.IsDead() || status.IsAggro())
            return;

        status.AggroOnPlayer();
        //navAgent.Resume();
    }

    void DeAggro()
    {
        if (status.IsDead() || status.IsIdle())
            return;

        status.BecomeIdle();
        //navAgent.Stop();
    }

    void Die()
    {
        navAgent.Stop();
    }

    private void Shoot()
    {
        lastShotTime = FireRate;
        GameObject.Instantiate(ProjectilePrefab, spellSource.position, Quaternion.LookRotation(player.position - spellSource.position));
        sounds.PlayFireSpellSound();
    }
}
