using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public int Health = 3;
    public int Mana = 10;
    private Checkpoint checkpoint;
    
	void Start () {
		
	}
	
	void Update () {
		
	}

    public void OutOfBounds()
    {
        print("You fell");
        transform.position = checkpoint.transform.position;
    }

    public void CheckpointTouched(Checkpoint checkpoint)
    {
        print("checkpoint touched!");
        if(this.checkpoint != null)
            this.checkpoint.SetInactive();
        this.checkpoint = checkpoint;
    }
}
