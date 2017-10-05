using System;
using UnityEngine;

[Serializable]
public class Fireball : Spell
{
    public Transform explosionPrefab;

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        this.MoveProjectile();
    }

    public override void Trigger()
    {
        Debug.Log("Destroyed " + this.name);
        //Instantiate(explosionPrefab);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter()
    {
        this.Trigger();
    }
}
