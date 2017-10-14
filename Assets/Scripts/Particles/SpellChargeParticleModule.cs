using System;
using UnityEngine;

public class SpellChargeParticleModule : MonoBehaviour {

	public ParticleSystem ChargingParticleEmitter;
	public GameObject FullyChargedParticleEmitter;
	public Light ChargingLight;

	public float surroundingParticleMaxRatePerSecond = 25f;
	private int numberOfFullyChargedParticleEmitters = 12;

	private float lightCurrentRange;
	private float lightMaxRange = 1.5f;

	private ParticleSystem.EmissionModule chargingParticleEmissionModule;
	private ParticleSystem[] fullyChargedParticleEmitters;

	void Start()
	{
		chargingParticleEmissionModule = ChargingParticleEmitter.emission;

		if (FullyChargedParticleEmitter != null)
			InitialiseParticleEmitters();
	}

	void Update()
	{
		if (lightCurrentRange > 0)
			AddLightFlicker();
	}

	private void AddLightFlicker()
	{
		ChargingLight.range = lightCurrentRange + Mathf.Sin(Time.deltaTime) * 10f;
	}

	public void EmitFullyChargedParticles()
	{
		foreach (ParticleSystem system in fullyChargedParticleEmitters)
		{
			system.Emit(1);
		}
	}

	public void EmitChargingParticles(float timeHeld, float maximumChargeTime)
	{
		var timeFraction = Mathf.Min(timeHeld, maximumChargeTime) / maximumChargeTime;
		chargingParticleEmissionModule.rateOverTime = timeFraction * surroundingParticleMaxRatePerSecond;

		lightCurrentRange = timeFraction * lightMaxRange;
		AddLightFlicker();
	}

	private void InitialiseParticleEmitters()
	{
		fullyChargedParticleEmitters = new ParticleSystem[numberOfFullyChargedParticleEmitters];

		for (int i = 0; i < numberOfFullyChargedParticleEmitters; i++)
		{
			var newParticleEffect = GameObject.Instantiate(FullyChargedParticleEmitter, FullyChargedParticleEmitter.transform.parent);

			var rotationQuaternion = Quaternion.AngleAxis((360 / numberOfFullyChargedParticleEmitters) * i, transform.up);
			newParticleEffect.transform.localPosition = rotationQuaternion * newParticleEffect.transform.localPosition;
			newParticleEffect.transform.localRotation = rotationQuaternion;

			fullyChargedParticleEmitters[i] = newParticleEffect.GetComponent<ParticleSystem>();
		}
	}

	internal void ClearAllEffects()
	{
		ClearParticleEffects();
		ClearLightEffects();
	}

	internal void ClearParticleEffects()
	{
		chargingParticleEmissionModule.rateOverTime = 0f;
	}

	internal void ClearLightEffects()
	{
		lightCurrentRange = 0f;
		ChargingLight.range = 0f;
	}
}
