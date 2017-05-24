using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public const int MaxHealth = 3;
    public const int MaxMana = 100;
    public int Health = 3;
    public int Mana = 100;
    private Checkpoint checkpoint;
 
    void Start () {
		
	}
	
	void Update () {
		
	}

    public void OutOfBounds()
    {
        Respawn();
    }

    public void BumpedEnemy()
    {
        TakeDamage(1);
    }

    public void CheckpointTouched(Checkpoint checkpoint)
    {
        if(this.checkpoint != null)
            this.checkpoint.SetInactive();
        this.checkpoint = checkpoint;
    }

    public void TakeDamage(int damage)
    {
        print("ouch");
        Health -= damage;
        if (Health <= 0)
            Respawn();
    }

    private void Respawn()
    {
        Health = MaxHealth;
        transform.position = checkpoint.transform.position;
    }
}