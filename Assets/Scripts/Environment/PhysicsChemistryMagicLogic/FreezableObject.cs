using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChemistryObject))]
public class FreezableObject : MonoBehaviour {

    [ReadOnly]
    [SerializeField]
    private bool frozen = false;

    [SerializeField]
    private float maxFrostHealth;
    [ReadOnly]
    [SerializeField]
    private float currentFrostHealth;
    
    [SerializeField]
    private float maxFrozenTime;
    [ReadOnly]
    [SerializeField]
    private float currentFrozenTime;
    
    public Material frozenMaterial;
    private Material normalMaterial;

    [SerializeField]
    protected AudioClip freezeSound;

    private float frostHealthRegenRate;

    private ChemistryObject chemObject;
    private MeshRenderer meshRenderer;

    protected virtual void Start () {
        frozen = false;
        currentFrostHealth = maxFrostHealth;
        currentFrozenTime = 0;

        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        normalMaterial = meshRenderer.material;
        frostHealthRegenRate = maxFrostHealth * .1f;
        chemObject = GetComponent<ChemistryObject>();
    }

    protected virtual void Update () {
        if (frozen)
        {
            currentFrozenTime += Time.deltaTime;
            if (currentFrozenTime >= maxFrozenTime)
                UnFreeze();
        }
        else if (currentFrostHealth < maxFrostHealth)
            currentFrostHealth += (frostHealthRegenRate * Time.deltaTime);
        else
            currentFrostHealth = maxFrostHealth;
            
	}

    public void TakeFrostDamage()
    {
        if (frozen)
            return;

        currentFrostHealth -= 1;
        if (currentFrostHealth <= 0)
            Freeze();
    }

    protected virtual void Freeze()
    {
        frozen = true;
        meshRenderer.material = frozenMaterial;
        currentFrozenTime = 0;
        chemObject.ChemistryInteraction(0, Element.Ice);
        AudioSource.PlayClipAtPoint(freezeSound, transform.position);
    }

    protected virtual void UnFreeze()
    {
        frozen = false;
        meshRenderer.material = normalMaterial;
        currentFrostHealth = maxFrostHealth;
        currentFrozenTime = 0;
    }
}
