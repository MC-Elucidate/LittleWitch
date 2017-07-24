using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMode : MonoBehaviour{

    public Spell fireballPrefab;
    public Sprite spellIcon;

    public void BasicAttackPressed(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObject.Instantiate(fireballPrefab, spawnPosition, spawnRotation);
    }
}
