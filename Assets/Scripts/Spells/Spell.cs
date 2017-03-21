using System;
using UnityEngine;

public abstract class Spell: MonoBehaviour
{
    public string spellName;
    public Sprite spellIcon;
    public string inputString;
    public Element element;
    public SpellType type;
    public float damage;
    public float range;
    public float aoe;
    public float speed;

    private float totalDistanceTravelled = 0f;

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
            this.transform.position += distanceToTravel;

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
    Wind
}

public enum SpellType
{
    AoE,
    Cone,
    Projectile,
    Ray,
    Self
}