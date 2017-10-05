using UnityEngine;

public class SpellChargeParticleModule : MonoBehaviour {

	public ParticleSystem ChargingParticleEmitter;
	public GameObject FullyChargedParticleEmitter;

	private float fullyChargedParticleSpeed = 15f;
	private float fullyChargedParticleSize = 0.1f;
	private float fullyChargedParticleLifetime = 0.5f;
	private int numberOfFullyChargedParticleEmitters = 12;

	private float surroundingParticleMaxRatePerSecond = 25f;

	private ParticleSystem.EmissionModule chargingParticleEmissionModule;
	private ParticleSystem[] fullyChargedParticleEmitters;

	void Start()
	{
		chargingParticleEmissionModule = ChargingParticleEmitter.emission;
		InitialiseParticleEmitters();
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
		chargingParticleEmissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(Mathf.Min(timeHeld, maximumChargeTime) / maximumChargeTime * surroundingParticleMaxRatePerSecond);
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
}
