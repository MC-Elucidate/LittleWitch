using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMode : MonoBehaviour {

    public Spell fireballPrefab;
    public Spell chargedFireballPrefab;
    public Sprite spellIcon;

    private float timeHeld = 0f;
    private float timeForChargedAttack = 3f;
    private bool isCharging = false;

    public void Update()
    {
        if (isCharging)
            timeHeld += Time.deltaTime;
    }

    public void BasicAttackPressed(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        isCharging = true;
    }

    public void BasicAttackReleased(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if(timeHeld >= timeForChargedAttack)
            GameObject.Instantiate(chargedFireballPrefab, spawnPosition, spawnRotation);
        else
            GameObject.Instantiate(fireballPrefab, spawnPosition, spawnRotation);

        isCharging = false;
        timeHeld = 0f;
    }
}
