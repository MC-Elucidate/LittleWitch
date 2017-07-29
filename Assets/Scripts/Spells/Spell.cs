using System;
using UnityEngine;

public abstract class Spell: MonoBehaviour
{
    public string spellName;
    public string inputString;
    public Element element;
    public SpellType type;
    public float damage;
    public float range;
    public float aoe;
    public float speed;

    public Transform castEffectPrefab;

    private float totalDistanceTravelled = 0f;
    private Rigidbody rigidbody;

    public virtual void Start()
    {
        this.rigidbody = this.transform.GetComponent<Rigidbody>();
        if(castEffectPrefab != null)
            Instantiate(castEffectPrefab, this.transform.position, this.transform.rotation);
    }

    //How the spell affects the world
    public virtual void Trigger()
    {
        Debug.Log("Spell has been triggered!");
    }

    public virtual void MoveProjectile()
    {
        if (type == SpellType.Projectile)
        {
            this.transform.DebugDirectionRay();
            var distanceToTravel = transform.forward * speed * Time.deltaTime;
            totalDistanceTravelled += distanceToTravel.magnitude;
            this.rigidbody.MovePosition(this.transform.position += distanceToTravel);

            if (totalDistanceTravelled >= range)
                Destroy(this.gameObject);
        }
        
    }
}

public enum Element
{
    Earth,
    Fire,
    Water,
    Wind,
    None
}

public enum SpellType
{
    AoE,
    Cone,
    Projectile,
    Ray,
    Self
}