using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HomingProjectile : Projectile
{
	private sealed class _OnTriggerEnter2D_c__AnonStorey0
	{
		internal GameObject particle;

		internal void __m__0()
		{
			this.particle.SetActive(false);
		}
	}

	private sealed class _OnTriggerEnter2D_c__AnonStorey1
	{
		internal GameObject particle;

		internal void __m__0()
		{
			this.particle.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Enemy component = other.GetComponent<Enemy>();
		if (component)
		{
			component.CallFlash(10.0, 5L, ProjectileType.Projectile);
			GameObject particle = ParticleObjectPooler.instance.GetPooledObject();
			particle.SetActive(true);
			particle.transform.position = base.transform.position;
			particle.transform.DOMove(particle.transform.position, 1.6f, false).OnComplete(delegate
			{
				particle.SetActive(false);
			});
			base.gameObject.SetActive(false);
		}
		if (other.tag == "Ground")
		{
			GameObject particle = ParticleObjectPooler.instance.GetPooledObject();
			particle.SetActive(true);
			particle.transform.position = base.transform.position;
			particle.transform.DOMove(particle.transform.position, 1.6f, false).OnComplete(delegate
			{
				particle.SetActive(false);
			});
			base.gameObject.SetActive(false);
		}
	}
}
