using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpliterChainProjectile : Projectile
{
	private sealed class _OnTriggerEnter2D_c__AnonStorey0
	{
		internal Enemy tt;

		internal bool __m__0(Collider2D e)
		{
			return e.GetComponent<Enemy>() != this.tt && e.GetComponent<Enemy>();
		}

		internal bool __m__1(Collider2D e)
		{
			return e.GetComponent<Enemy>() != this.tt && e.GetComponent<Enemy>();
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
				Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1f);
				int num = (from e in array
				where e.GetComponent<Enemy>() != tt && e.GetComponent<Enemy>()
				select e).Count<Collider2D>();
				Collider2D[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					Collider2D collider2D = array2[i];
					int coefLevel_ = BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel);
					Enemy component = collider2D.GetComponent<Enemy>();
					if (collider2D.GetComponent<Enemy>())
					{
						if (component == tt)
						{
							SoundController.instance.PlaySoundItem2();
							component.CallFlash((double)(BaseValue.spliter_chain_base_damage * (long)coefLevel_ / 4L), BaseValue.coin_per_item2_hit / 4L, ProjectileType.Non_Projectile);
						}
						else
						{
							component.CallFlash((double)((long)((float)(BaseValue.spliter_chain_base_damage * (long)coefLevel_) / (100f / (float)BaseValue.damage_percent_item * (float)num) / 4f)), (long)((float)BaseValue.coin_per_item2_hit / (100f / (float)BaseValue.damage_percent_item * 4f * (float)num)), ProjectileType.Non_Projectile);
						}
					}
				}
				GameObject pooledObject = ParticleObjectPooler.instance.GetPooledObject("item2_particle");
				pooledObject.SetActive(true);
				pooledObject.transform.position = base.transform.position;
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
				SoundController.instance.PlaySoundItem2();
				Collider2D[] array3 = Physics2D.OverlapCircleAll(base.transform.position, 1f);
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
						component2.CallFlash((double)((long)((float)(BaseValue.spliter_chain_base_damage * (long)coefLevel_2) / (100f / (float)BaseValue.damage_percent_item * (float)num2 * 4f))), BaseValue.coin_per_item2_hit / (long)(8 * num2), ProjectileType.Non_Projectile);
					}
				}
				GameObject pooledObject2 = ParticleObjectPooler.instance.GetPooledObject("item2_particle");
				pooledObject2.SetActive(true);
				pooledObject2.transform.position = base.transform.position;
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
