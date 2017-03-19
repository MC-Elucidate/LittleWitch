using System;
using UnityEngine;

[Serializable]
public class Fireball : Spell
{
    void Start()
    {
        
    }

    void Update()
    {
        this.MoveProjectile();
    }

    public override void Trigger()
    {
        Debug.Log("Destroyed " + this.name);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter()
    {
        this.Trigger();
    }
}
