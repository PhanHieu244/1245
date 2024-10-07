using System;
using System.Collections.Generic;
using UnityEngine;

public class AssignColliders : MonoBehaviour
{
	private ParticleSystem ps;

	private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

	private List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

	private void OnEnable()
	{
		this.ps = base.GetComponent<ParticleSystem>();
	}

	private void OnParticleTrigger()
	{
		int triggerParticles = this.ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, this.enter);
		int triggerParticles2 = this.ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, this.exit);
		for (int i = 0; i < triggerParticles; i++)
		{
			ParticleSystem.Particle value = this.enter[i];
			UnityEngine.Debug.Log(value.position);
			this.enter[i] = value;
		}
		this.ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, this.enter);
		this.ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, this.exit);
	}
}
