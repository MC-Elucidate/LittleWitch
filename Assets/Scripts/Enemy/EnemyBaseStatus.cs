using UnityEngine;

[RequireComponent(typeof(DamageIndicator))]
public class EnemyBaseStatus : ChemistryObject {

    [SerializeField]
    protected float Health;
    public EAIState state { get; private set; }

    [SerializeField]
    private GameObject deathPoof;

    private DamageIndicator damageIndicator;

    void Start () {
        state = EAIState.Idle;
        damageIndicator = gameObject.GetComponent<DamageIndicator>();
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
        else if(damage > 0)
            damageIndicator.TakeDamage();
    }

    protected virtual void Die()
    {
        state = EAIState.Dead;
        Destroy(Instantiate(deathPoof, transform.position, Quaternion.identity), 2f);
        Destroy(gameObject);
    }
    #endregion

    #region AIState
    public bool IsDead()
    {
        return state == EAIState.Dead;
    }

    public bool IsAggro()
    {
        return state == EAIState.Aggro;
    }

    public bool IsIdle()
    {
        return state == EAIState.Idle;
    }

    public void AggroOnPlayer()
    {
        state = EAIState.Aggro;
    }

    public void BecomeIdle()
    {
        state = EAIState.Idle;
    }

    public void Freeze()
    {
        state = EAIState.Frozen;
    }

    public void UnFreeze()
    {
        state = EAIState.Idle;
    }

    public bool IsFrozen()
    {
        return state == EAIState.Frozen;
    }
    #endregion
}
