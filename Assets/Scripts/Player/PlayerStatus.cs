using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    [SerializeField]
    public const int MaxHealth = 6;
    [SerializeField]
    public const int MaxMana = 100;

    public int Health { get; private set; }
    public float Mana { get; private set; }
    public int Gems { get; private set; }

    [ReadOnly]
    public PlayerState state;

	private Checkpoint checkpoint;
 
	void Start () {
        Health = MaxHealth;
        Mana = MaxMana;
        Gems = 0;
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

	public bool UseFocus(float amount)
	{
		if (amount <= Mana)
		{
			Mana -= amount;
			if (Mana < 0)
				Mana = 0;
			return true;
		}
		else
		{
			return false;
		}
	}

	public void RegenFocus(float amount)
	{
		Mana += amount;
		if (Mana > MaxMana)
			Mana = MaxMana;
	}

	public void AddGems(int amount)
	{
		Gems += amount;
	}

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
	
}