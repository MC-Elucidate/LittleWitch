using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMode : ASpellMode
{

    public Spell fireballPrefab;
    public Spell chargedFireballPrefab;
    

    private float timeHeld = 0f;
    private float timeForChargedAttack = 3f;
    private bool isCharging = false;

    public void Update()
    {
        if (isCharging)
            timeHeld += Time.deltaTime;
    }

    public override void AttackPressed(Vector3 spawnPosition, Vector3 spawnDirection)
    {
        isCharging = true;
    }

    public override void AttackReleased(Vector3 spawnPosition, Vector3 spawnDirection)
    {
        if(timeHeld >= timeForChargedAttack)
            GameObject.Instantiate(chargedFireballPrefab, spawnPosition, Quaternion.LookRotation(spawnDirection, Vector3.up));
        else
            GameObject.Instantiate(fireballPrefab, spawnPosition, Quaternion.LookRotation(spawnDirection, Vector3.up));

        isCharging = false;
        timeHeld = 0f;
    }
}
