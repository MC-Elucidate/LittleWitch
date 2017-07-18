using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolPointsCreature : MonoBehaviour {

    public Vector3[] PatrolPoints;
    public float WaitTime;
    private NavMeshAgent navAgent;
    private int currentPointIndex;
    private bool isMoving;

    void Start () {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        if (PatrolPoints.Length > 1)
        {
            navAgent.SetDestination(PatrolPoints[0]);
            navAgent.Resume();
            currentPointIndex = 0;
            isMoving = true;
        }
        else
        {
            navAgent.Stop();
            isMoving = false;
        }
    }
	
	void Update () {
        if (isMoving && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            isMoving = false;
            navAgent.Stop();
            Invoke("SetNextDestinationPoint", WaitTime);
        }
	}

    void SetNextDestinationPoint()
    {
        ++currentPointIndex;
        if (currentPointIndex >= PatrolPoints.Length)
            currentPointIndex = 0;
        navAgent.SetDestination(PatrolPoints[currentPointIndex]);
        navAgent.Resume();
        isMoving = true;
    }
}
