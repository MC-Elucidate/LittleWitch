using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemyStatus : EnemyBase {

    private enum AIState
    {
        Idle,
        Dead
    }

    private AIState state;
    
    void Start () {
        state = AIState.Idle;
    }
    
    void Update () {
		
	}

    protected override void Die()
    {
        state = AIState.Dead;
        Destroy(gameObject, 1.5f);
    }


    public bool IsDead()
    {
        return state == AIState.Dead;
    }

    public bool IsIdle()
    {
        return state == AIState.Idle;
    }

    public void BecomeIdle()
    {
        state = AIState.Idle;
    }
}
