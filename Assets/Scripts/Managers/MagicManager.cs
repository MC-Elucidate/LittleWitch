using System;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    public SpellBooleanPair[] spellList;
    public Transform readySpellPrefab = null;

    private const bool ALL_SPELLS_UNLOCKED = true;

    public bool CheckValidSpellInputs(string inputString, Transform targetSpawnPosition)
    {
        foreach (SpellBooleanPair pair in spellList)
        {
            if (inputString.Contains(pair.spellPrefab.GetComponent<ISpell>().inputString) && (pair.unlocked || ALL_SPELLS_UNLOCKED))
            {
                readySpellPrefab = pair.spellPrefab;
                CastSpell(targetSpawnPosition);
                return true;
            }
        }
        return false;
    }

    private void CastSpell(Transform targetSpawnPosition)
    {
        //Casting logic and animations go over here probably
        if (readySpellPrefab != null)
        {
            GameObject.Instantiate(readySpellPrefab, targetSpawnPosition.position, targetSpawnPosition.rotation);
            readySpellPrefab = null;
        }
        else
        {
            //Play no magic spell sound
        }
    }

    [Serializable]
    public class SpellBooleanPair
    {
        public Transform spellPrefab;
        public bool unlocked;
    }
}
