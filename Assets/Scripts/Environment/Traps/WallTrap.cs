using System;
using System.Linq;
using UnityEngine;

public class WallTrap : Trap
{
    public Transform[] wallPieces;
    public Vector3[] wallPieceTranslation;
    public TrapCondition trapCondition;

    public float trapDuration;
    public GameObject[] enemiesToKill;

    private float currentTrapTime = 0f;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (trapSprung)
        {
            Debug.Log("Checking WallTrap conditions!");

            if (trapCondition == TrapCondition.Timer)
            {
                currentTrapTime += Time.deltaTime;
                if (currentTrapTime >= trapDuration)
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
        if (!trapSprung && !trapCleared && !trapWaitingForReset)
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

            trapWaitingForReset = true;
            Invoke("WaitAfterReset", trapResetDelay);
        }
    }

    private void WaitAfterReset()
    {
        trapWaitingForReset = false;
    }

    public enum TrapCondition { Timer, Enemies }
}
