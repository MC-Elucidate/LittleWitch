using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASpellMode : MonoBehaviour {

    public Sprite spellIcon;

    void Start () {
		
	}
	
	void Update () {
		
	}
    public abstract void AttackPressed(Vector3 spawnPosition, Vector3 spawnDirection, Vector3? targetPosition = null);

    public abstract void AttackReleased(Vector3 spawnPosition, Vector3 spawnDirection, Vector3? targetPosition = null);

    public abstract void OnSpellChangedFrom();
}
