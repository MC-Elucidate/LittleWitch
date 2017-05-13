using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public int Health = 3;
    public int Mana = 10;
    private Transform checkpoint;
    
	void Start () {
		
	}
	
	void Update () {
		
	}

    public void OutOfBounds()
    {
        print("You fell");
        transform.position = checkpoint.position;
    }

    public void CheckpointTouched(Transform checkpoint)
    {
        print("checkpoint touched!");
        this.checkpoint = checkpoint;
    }
}
