using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Item6Projectile : Projectile
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
				Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 4f);
				int num = (from e in array
				where e.GetComponent<Enemy>() != tt && e.GetComponent<Enemy>()
				select e).Count<Collider2D>();
				Collider2D[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					Collider2D collider2D = array2[i];
					Enemy component = collider2D.GetComponent<Enemy>();
					if (component)
					{
						int coefLevel_ = BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel);
						if (component == tt)
						{
							SoundController.instance.StopSoundItem6Launch();
							SoundController.instance.PlaySoundItem6();
							component.CallFlash((double)(BaseValue.item6_base_damage * (long)coefLevel_), BaseValue.coin_per_item6_hit, ProjectileType.Non_Projectile);
						}
						else
						{
							component.CallFlash((double)((long)((float)(BaseValue.item6_base_damage * (long)coefLevel_) / (100f / (float)BaseValue.damage_percent_item * (float)num))), (long)((float)BaseValue.coin_per_item6_hit / (100f / (float)BaseValue.damage_percent_item * (float)num)), ProjectileType.Non_Projectile);
						}
					}
				}
				GameObject pooledObject = ParticleObjectPooler.instance.GetPooledObject("item6_particle");
				pooledObject.SetActive(true);
				pooledObject.transform.position = base.transform.position;
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
				SoundController.instance.StopSoundItem6Launch();
				SoundController.instance.PlaySoundItem6();
				Collider2D[] array3 = Physics2D.OverlapCircleAll(base.transform.position, 4f);
				int num2 = (from e in array3
				where e.GetComponent<Enemy>() != tt && e.GetComponent<Enemy>()
				select e).Count<Collider2D>();
				UnityEngine.Debug.Log(num2);
				Collider2D[] array4 = array3;
				for (int j = 0; j < array4.Length; j++)
				{
					Collider2D collider2D2 = array4[j];
					Enemy component2 = collider2D2.GetComponent<Enemy>();
					if (component2)
					{
						int coefLevel_2 = BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel);
						component2.CallFlash((double)((long)((float)(BaseValue.item6_base_damage * (long)coefLevel_2) / (100f / (float)BaseValue.damage_percent_item * (float)num2))), (long)((float)BaseValue.coin_per_item6_hit / (100f / (float)BaseValue.damage_percent_item * (float)num2)), ProjectileType.Non_Projectile);
					}
				}
				GameObject pooledObject2 = ParticleObjectPooler.instance.GetPooledObject("item6_particle");
				pooledObject2.SetActive(true);
				pooledObject2.transform.position = base.transform.position;
				base.gameObject.SetActive(false);
			}
			else
			{
				this.isInCollision = false;
			}
		}
	}
}
