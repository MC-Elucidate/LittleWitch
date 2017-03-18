using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    public SpellBooleanPair[] spellList;

    private const bool ALL_SPELLS_UNLOCKED = true;

    void Start()
    {

    }

    void Update()
    {

    }

    public bool Cast(string inputString, Vector3 targetSpawnPosition)
    {
        //if (string.IsNullOrEmpty(inputString))
       //     return false;

        foreach (SpellBooleanPair pair in spellList)
        {
            if (inputString.Contains(pair.spellPrefab.GetComponent<ISpell>().inputString))
            {
                GameObject.Instantiate(pair.spellPrefab, targetSpawnPosition, Quaternion.identity);
                return true;
            }
        }

        return false;
    }

    [Serializable]
    public class SpellBooleanPair
    {
        public Transform spellPrefab;
        public bool unlocked;
    }
}

public interface ISpell
{
    string name { get; set; }
    string inputString { get; set; }
    Element element { get; set; }
    SpellType type { get; set; }
    float damage { get; set; }
    float range { get; set; }
    float aoe { get; set; }

    void Trigger();
}

//This should be an interface itself or something to inherit from
public enum Element
{
    Earth,
    Fire,
    Water,
    Wind
}

public enum SpellType
{
    AoE,
    Cone,
    Projectile,
    Ray,
    Self
}
