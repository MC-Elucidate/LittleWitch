using System;
using System.Linq;
using UnityEngine;

public class WallTrap : Trap
{
    public Transform[] wallPieces;
    public Vector3[] wallPieceTranslation;
    public TrapCondition trapCondition;

    //We need conditions over here - Time before it resets, enemies to kill, etc
    public float trapResetTime;
    public GameObject[] enemiesToKill;

    private float currentTrapTime = 0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (trapSprung)
        {
            Debug.Log("Checking WallTrap conditions!");

            if (trapCondition == TrapCondition.Timer)
            {
                currentTrapTime += Time.deltaTime;
                if (currentTrapTime >= trapResetTime)
                {
                    ResetTrap();
                }
            }
            else if (trapCondition == TrapCondition.Enemies)
            {
                Debug.Log("Enemies = " + enemiesToKill == null ? 0 : enemiesToKill.Length);

                if (enemiesToKill.All(enemy => enemy == null)) //Check if the associated game objects have been deleted
                {
                    Debug.Log("All enemies killed!");
                    ResetTrap();
                }
            }
        }
	}

    public override void SpringTrap()
    {
        if (!trapSprung && !trapCleared)
        {
            Debug.Log("Trap is Sprung!");

            trapSprung = true;
            gameObject.SetActive(true);

            for (int i = 0; i < wallPieces.Length; i++)
            {
                wallPieces[i].Translate(wallPieceTranslation[i]); //This could be better with Root Motion
            }
        }
    }

    public override void ResetTrap()
    {
        if (trapSprung)
        {
            Debug.Log("Trap is Reset!");
            currentTrapTime = 0f;
            //Instantiate enemies again

            gameObject.SetActive(false);

            for (int i = 0; i < wallPieces.Length; i++)
            {
                wallPieces[i].Translate(-wallPieceTranslation[i]); //This could be better with Root Motion
            }

            trapSprung = false;

            if (!trapCanRetrigger)
                trapCleared = true;
        }
    }

    public enum TrapCondition { Timer, Enemies }
}
