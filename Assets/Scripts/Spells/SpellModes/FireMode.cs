using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMode : ASpellMode
{
	public Spell fireballPrefab;
	public Spell chargedFireballPrefab;
	public SpellChargeParticleModule spellChargeParticleModule;

	private float timeHeld = 0f;
	private float timeForChargedAttack = 3f;
	private bool isCharging = false;
	private bool fullyCharged = false;

	public void Update()
	{
		if (isCharging)
		{
			timeHeld += Time.deltaTime;
			spellChargeParticleModule.EmitChargingParticles(timeHeld, timeForChargedAttack);
		}

		if (timeHeld >= timeForChargedAttack && !fullyCharged)
		{
			fullyCharged = true;
			spellChargeParticleModule.EmitFullyChargedParticles();
		}
	}

	public override void AttackPressed(Vector3 spawnPosition, Vector3 spawnDirection, Vector3? targetPosition = null)
	{
		isCharging = true;
	}

	public override void AttackReleased(Vector3 spawnPosition, Vector3 spawnDirection, Vector3? targetPosition = null)
	{
		if (timeHeld >= timeForChargedAttack)
			GameObject.Instantiate(chargedFireballPrefab, spawnPosition, Quaternion.LookRotation(spawnDirection, Vector3.up));
		else
			GameObject.Instantiate(fireballPrefab, spawnPosition, Quaternion.LookRotation(spawnDirection, Vector3.up));

		ResetAttack();
	}

	private void ResetAttack()
	{
		isCharging = false;
		fullyCharged = false;
		timeHeld = 0f;
		spellChargeParticleModule.ClearAllEffects();
	}
}
