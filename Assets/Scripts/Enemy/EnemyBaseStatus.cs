using UnityEngine;

public class EnemyBaseStatus : ChemistryObject {

    [SerializeField]
    protected float Health;
    private AIState state;

    void Start () {
        state = AIState.Idle;
    }

    void Update () {
		
	}

    #region ElementInteractions
    protected override void FireInteraction()
    {
        print("My hair is on fire!");
    }

    protected override void WaterInteraction()
    {
        print("Dance, Water! Dance!");
    }

    protected override void EarthInteraction()
    {
        print("Rock you like a hurricane!");
    }

    protected override void WindInteraction()
    {
        print("Quick as the wind!");
    }

    protected override void IceInteraction()
    {
        print("Ice to meet you...");
    }
    #endregion

    #region HealthState
    protected override void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        state = AIState.Dead;
        Destroy(gameObject, 1.5f);
    }
    #endregion

    #region AIState
    public bool IsDead()
    {
        return state == AIState.Dead;
    }

    public bool IsAggro()
    {
        return state == AIState.Aggro;
    }

    public bool IsIdle()
    {
        return state == AIState.Idle;
    }

    public void AggroOnPlayer()
    {
        state = AIState.Aggro;
    }

    public void BecomeIdle()
    {
        state = AIState.Idle;
    }
    #endregion

    private enum AIState
    {
        Idle,
        Aggro,
        Dead
    }
}
