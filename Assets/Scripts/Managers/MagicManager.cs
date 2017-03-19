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
            if (inputString.Contains(pair.spellPrefab.GetComponent<Spell>().inputString) && (pair.unlocked || ALL_SPELLS_UNLOCKED))
            {
                readySpellPrefab = pair.spellPrefab;
                Debug.Log(String.Format("Spell {0} has been readied!", readySpellPrefab.name));
                return true;
            }
        }
        return false;
    }

    public void CastSpell(Transform targetSpawnPosition)
    {
        //Casting logic and animations go over here probably
        if (readySpellPrefab != null)
        {
            Debug.Log(String.Format("Spell {0} has been cast!", readySpellPrefab.name));
            GameObject.Instantiate(readySpellPrefab, targetSpawnPosition.position, targetSpawnPosition.rotation);
            readySpellPrefab = null;
        }
        else
        {
            Debug.Log("No spell is ready");
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
