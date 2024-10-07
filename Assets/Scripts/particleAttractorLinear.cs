using System;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class particleAttractorLinear : MonoBehaviour
{
	private ParticleSystem ps;

	private ParticleSystem.Particle[] m_Particles;

	public Transform target;

	public float speed = 5f;

	private int numParticlesAlive;

	private void Start()
	{
		this.ps = base.GetComponent<ParticleSystem>();
		if (!base.GetComponent<Transform>())
		{
			base.GetComponent<Transform>();
		}
	}

	private void Update()
	{
		this.m_Particles = new ParticleSystem.Particle[this.ps.main.maxParticles];
		this.numParticlesAlive = this.ps.GetParticles(this.m_Particles);
		float t = this.speed * Time.deltaTime;
		for (int i = 0; i < this.numParticlesAlive; i++)
		{
			this.m_Particles[i].position = Vector3.LerpUnclamped(this.m_Particles[i].position, this.target.position, t);
		}
		this.ps.SetParticles(this.m_Particles, this.numParticlesAlive);
	}
}
