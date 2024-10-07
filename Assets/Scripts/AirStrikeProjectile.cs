using DG.Tweening;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AirStrikeProjectile : Projectile
{
	private sealed class _OnTriggerEnter2D_c__AnonStorey0
	{
		internal Enemy tt;
	}

	private sealed class _OnTriggerEnter2D_c__AnonStorey1
	{
		internal GameObject particle;

		internal AirStrikeProjectile._OnTriggerEnter2D_c__AnonStorey0 __f__ref_0;

		internal bool __m__0(Collider2D e)
		{
			return e.GetComponent<Enemy>() != this.__f__ref_0.tt && e.GetComponent<Enemy>();
		}

		internal void __m__1()
		{
			this.particle.SetActive(false);
		}
	}

	private sealed class _OnTriggerEnter2D_c__AnonStorey2
	{
		internal GameObject particle;

		internal AirStrikeProjectile._OnTriggerEnter2D_c__AnonStorey0 __f__ref_0;

		internal bool __m__0(Collider2D e)
		{
			return e.GetComponent<Enemy>() != this.__f__ref_0.tt && e.GetComponent<Enemy>();
		}

		internal void __m__1()
		{
			this.particle.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Enemy tt = other.GetComponent<Enemy>();
		if (tt)
		{
			if (!this.isInCollision)
			{
				this.isInCollision = true;
				Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 2f);
				int num = (from e in array
				where e.GetComponent<Enemy>() != tt && e.GetComponent<Enemy>()
				select e).Count<Collider2D>();
				Collider2D[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					Collider2D collider2D = array2[i];
					Enemy component = collider2D.GetComponent<Enemy>();
					if (collider2D.GetComponent<Enemy>())
					{
						int coefLevel_ = BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel);
						if (component == tt)
						{
							SoundController.instance.PlaySoundItem3();
							component.CallFlash((double)(BaseValue.air_strike_base_damage * (long)coefLevel_ / 5L), BaseValue.air_strike_base_damage * (long)BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel) / 5L, ProjectileType.Non_Projectile);
						}
						else
						{
							component.CallFlash((double)((long)((float)(BaseValue.air_strike_base_damage * (long)coefLevel_) / (100f / (float)BaseValue.damage_percent_item * (float)num) / 5f)), (long)((float)(BaseValue.air_strike_base_damage * (long)coefLevel_) / (100f / (float)BaseValue.damage_percent_item * (float)num) / 5f), ProjectileType.Non_Projectile);
						}
					}
				}
				GameObject particle = ParticleObjectPooler.instance.GetPooledObject("item3_particle");
				particle.SetActive(true);
				particle.transform.position = base.transform.position;
				particle.transform.DOMove(particle.transform.position, 0.9f, false).OnComplete(delegate
				{
					particle.SetActive(false);
				});
				base.GetComponent<TrailRenderer>().Clear();
				base.gameObject.SetActive(false);
			}
			else
			{
				this.isInCollision = false;
			}
		}
		if (other.tag == "Ground")
		{
			if (!this.isInCollision)
			{
				this.isInCollision = true;
				SoundController.instance.PlaySoundItem3();
				Collider2D[] array3 = Physics2D.OverlapCircleAll(base.transform.position, 2f);
				int num2 = (from e in array3
				where e.GetComponent<Enemy>() != tt && e.GetComponent<Enemy>()
				select e).Count<Collider2D>();
				Collider2D[] array4 = array3;
				for (int j = 0; j < array4.Length; j++)
				{
					Collider2D collider2D2 = array4[j];
					Enemy component2 = collider2D2.GetComponent<Enemy>();
					if (component2)
					{
						int coefLevel_2 = BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel);
						component2.CallFlash((double)(BaseValue.air_strike_base_damage * (long)coefLevel_2 / (long)(2 * num2 * 5)), BaseValue.coin_per_item3_hit / (long)(10 * num2), ProjectileType.Non_Projectile);
					}
				}
				GameObject particle = ParticleObjectPooler.instance.GetPooledObject("item3_particle");
				particle.SetActive(true);
				particle.transform.position = base.transform.position + new Vector3(0f, 0.5f);
				particle.transform.DOMove(particle.transform.position, 0.9f, false).OnComplete(delegate
				{
					particle.SetActive(false);
				});
				base.GetComponent<TrailRenderer>().Clear();
				base.gameObject.SetActive(false);
			}
			else
			{
				this.isInCollision = false;
			}
		}
	}
}
