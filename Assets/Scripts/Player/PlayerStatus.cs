using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public const int MaxHealth = 3;
    public const int MaxMana = 100;
    public int Health = 3;
    public int Mana = 100;
    public PlayerState state;
    private Checkpoint checkpoint;

    public enum PlayerState
    {
        FreeMovement,
        Aiming,
        Dead
    }
 
    void Start () {
        state = PlayerState.FreeMovement;
	}
	
	void Update () {
		
	}

    public void OutOfBounds()
    {
        Respawn();
    }

    public void CheckpointTouched(Checkpoint checkpoint)
    {
        if(this.checkpoint != null)
            this.checkpoint.SetInactive();
        this.checkpoint = checkpoint;
    }
    
    public void Respawn()
    {
        Health = MaxHealth;
        transform.position = checkpoint.transform.position;
    }

    public void EnterAimMode() { state = PlayerState.Aiming; }

    public void LeaveAimMode() { state = PlayerState.FreeMovement; }
    
}