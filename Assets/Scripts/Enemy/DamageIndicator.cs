using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour {

    [SerializeField]
    private float damageFlashDuration;
    [SerializeField]
    private Material damagedMaterial;
    private Material normalMaterial;
    private MeshRenderer meshRenderer;

    private EnemyBaseStatus status;
    private FreezableEnemy freezableEnemy;

    void Start () {
        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        normalMaterial = meshRenderer.material;
        status = gameObject.GetComponent<EnemyBaseStatus>();
        freezableEnemy = gameObject.GetComponent<FreezableEnemy>();
    }
	
	void Update () {
		
	}

    public void TakeDamage()
    {
        StartCoroutine(DamageCoroutine());
    }

    private IEnumerator DamageCoroutine()
    {
        ShowDamageMaterial();
        yield return new WaitForSecondsRealtime(damageFlashDuration);
        ShowNormalMaterial();
    }

    private void ShowDamageMaterial()
    {
        meshRenderer.material = damagedMaterial;
    }

    private void ShowNormalMaterial()
    {
        if (status.state != EAIState.Frozen)
            meshRenderer.material = normalMaterial;
        else
            meshRenderer.material = freezableEnemy.frozenMaterial;
    }
}
