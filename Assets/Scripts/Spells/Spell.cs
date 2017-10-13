using System;
using UnityEngine;

public abstract class Spell: MonoBehaviour
{
    public string spellName;
    public string inputString;
    public Element element;
    public float damage;
    public float range;
    public float aoe;
    public float speed;

    public Transform castEffectPrefab;

    protected Rigidbody rigidBody;

    public virtual void Start()
    {
        this.rigidBody = this.transform.GetComponent<Rigidbody>();
        if(castEffectPrefab != null)
            Instantiate(castEffectPrefab, this.transform.position, this.transform.rotation);
    }

    //How the spell affects the world
    public virtual void Trigger()
    {
        Debug.Log("Spell has been triggered!");
    }
}